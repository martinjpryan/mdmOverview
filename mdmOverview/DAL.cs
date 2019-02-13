using mdmOverview.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
using System.Data.Entity;
using mdmOverview.Models.MoyPark;

namespace mdmOverview
{
    public class DAL
    {

        private mDBcontext db = new mDBcontext();


        public dtoProduct getProduct(string id)
        {
            string sql = @"SELECT TOP(1) [Id] ,[Action],[EntityNumber],[IdocNumber],[ServiceId],[StatusId],[Data]
      ,[StatusMessage]
      ,[Type]
      ,[Created]
      ,[CreatedBy]
      ,[Modified]
      ,[ModifiedBy]
      ,[Version]
        FROM[jbsmstr].[scoMaster].[RawInterfaceData]
        WHERE Id = '" + id.ToString() + "'";
            List<dtoSite> sites = new List<dtoSite>();
            List<dtoUnit> units = new List<dtoUnit>();
            RawInterfaceData r = db.Database.SqlQuery<RawInterfaceData>(sql).FirstOrDefault();
            string materialdesc = "No Material Description";
            string legacycode = "No Legacy Code";
            string pricingunit = "No Pricing Unit";
            string sapdocid = "No SAP Doc ID";
            string materialid = "No Material ID";

            if (r.Data != null)
            {
                try
                {
                    Material_Master_Data result;
                    var serializer = new XmlSerializer(typeof(Material_Master_Data));

                    using (var reader = new StringReader(r.Data))
                    {
                        result = (Material_Master_Data)serializer.Deserialize(reader);
                    }
                   

                    if (result.MaterialMasterGeneralData != null)
                    {
                        if (result.MaterialMasterGeneralData.LegacyCode != null) { legacycode = result.MaterialMasterGeneralData.LegacyCode.ToString(); }
                        if (result.MaterialMasterGeneralData.MaterialNumber != null) { materialid = result.MaterialMasterGeneralData.MaterialNumber.ToString(); }
                    }

                    if (result.SAPIDOCNumber != null)
                    {
                        sapdocid = result.SAPIDOCNumber;
                    }

                    if (result.MaterialMasterAlternateUoMDetails != null)
                    {
                        if (result.MaterialMasterAlternateUoMDetails.Count > 0)
                        {
                            foreach(Material_Master_DataMaterialMasterAlternateUoM u in result.MaterialMasterAlternateUoMDetails)
                            {
                                dtoUnit ui = new dtoUnit();
                                if(u.AlternateUoM != null)
                                {
                                    ui.pricingUM = u.AlternateUoM;
                                }
                                units.Add(ui);
                            }
                        }
                    }






                    if (result.MaterialMasterPlantDataDetails != null)
                    {
                        if (result.MaterialMasterPlantDataDetails.Count > 0)
                        {
                            foreach(Material_Master_DataMaterialMasterPlantData pd in result.MaterialMasterPlantDataDetails)
                            {
                                dtoSite s = new dtoSite();
                                if(pd.PlantCode != null)
                                {
                                    s.legacysitecode = pd.PlantCode;
                                    Plant pl = db.Plants.Where(i => i.plantcode == pd.PlantCode).FirstOrDefault();
                                    if (pl != null)
                                    {
                                        s.legacysitedesc = pl.plantdescription;
                                        s.sapsitecode = pl.plantcode;
                                    }
                                    sites.Add(s);
                                }
                            }
                            legacycode = result.MaterialMasterGeneralData.LegacyCode.ToString();
                        }
                    }


                    if (result.MaterialMasterShortTextsDetails.MaterialMasterShortTexts != null)
                    {
                        materialdesc = result.MaterialMasterShortTextsDetails.MaterialMasterShortTexts.MaterialDescription.ToString();
                    }
                    r.Data = materialdesc + " " + legacycode;
                }
                catch (Exception ex)
                {
                    string issue = ex.ToString();
                    r.Data = "Problem";
                }
            }

            dtoProduct p = new dtoProduct();
            p.sites = sites;
            p.units = units;
            p.materialid = materialid;
            p.recguid = r.Id.ToString();
            p.legacycode = legacycode;
            p.description = materialdesc;
            p.sapdocid = sapdocid;

            return p;
        }


        public dtoRawInterfaceDataSummary getHomeData()
        {
            dtoRawInterfaceDataSummary dto = new dtoRawInterfaceDataSummary();
            string sql = @"SELECT TOP(1000) [Id] ,[Action],[EntityNumber],[IdocNumber],[ServiceId],[StatusId],[Data]
      ,[StatusMessage]
      ,[Type]
      ,[Created]
      ,[CreatedBy]
      ,[Modified]
      ,[ModifiedBy]
      ,[Version]
        FROM[jbsmstr].[scoMaster].[RawInterfaceData]
        WHERE[type] = 'Material Master Data' AND created >= DateAdd(day,-8, getDate()) ORDER BY Created DESC
";
            List<RawInterfaceData> rData = db.Database.SqlQuery<RawInterfaceData>(sql).ToList();
            dto.errorcount = rData.Where(i => i.Action == "ERROR").Count();
            dto.insertcount = rData.Where(i => i.Action == "INSERT").Count();
            dto.updatecount = rData.Where(i => i.Action == "UPDATE").Count();

            dto.insertmax = 0;
            dto.updatemax = 0;

            for (int i = 0; i < 10; i++)
            {
                dtoGraphStat g = new dtoGraphStat();
                DateTime d = DateTime.Now.AddDays(-i);
                g.x = d.DayOfWeek.ToString().Substring(0,1).ToUpper().ToString();
                int c = rData.Where(j => j.Action == "INSERT").Where(j => j.Created.UtcDateTime.ToShortDateString() == d.ToShortDateString()).Count();
                if(c > dto.insertmax) { dto.insertmax = c; }
                g.y = c.ToString();
                g.desc = d.ToShortDateString() + " [" + c.ToString() + "]";
                dto.graphInserts.Add(g);
            }

            for (int i = 0; i < 10; i++)
            {
                dtoGraphStat g = new dtoGraphStat();
                DateTime d = DateTime.Now.AddDays(-i);
                g.x = d.DayOfWeek.ToString().Substring(0, 1).ToUpper().ToString();
                int c = rData.Where(j => j.Action == "UPDATE").Where(j => j.Created.UtcDateTime.ToShortDateString() == d.ToShortDateString()).Count();
                if (c > dto.updatemax) { dto.updatemax = c; }
                g.y = c.ToString();
                g.desc = d.ToShortDateString() + " [" + c.ToString() + "]";
                dto.graphUpdates.Add(g);
            }

            dto.errors = rData.Where(i => i.Action == "ERROR").OrderByDescending(i => i.Modified).Take(10).ToList();
            dto.inserts = rData.Where(i => i.Action == "INSERT").OrderByDescending(i => i.Modified).Take(10).ToList();
            dto.updates = rData.Where(i => i.Action == "UPDATE").OrderByDescending(i => i.Modified).Take(10).ToList();

            if ((dto.updatemax + dto.updatemax) <= 10) { dto.updatemax = 10; }
            if ((dto.insertmax + dto.insertmax) <= 10) { dto.insertmax = 10; }

            foreach (RawInterfaceData r in dto.inserts)
            {
                if(r.Data != null)
                {
                    try
                    {
                        Material_Master_Data result;
                        var serializer = new XmlSerializer(typeof(Material_Master_Data));

                        using (var reader = new StringReader(r.Data))
                        {
                            result = (Material_Master_Data)serializer.Deserialize(reader);
                        }
                        string materialdesc = "No Material Description";
                        string legacycode = "No Legacy Code";

                        if (result.MaterialMasterGeneralData != null)
                        {
                            if (result.MaterialMasterGeneralData.LegacyCode != null) { legacycode = result.MaterialMasterGeneralData.LegacyCode.ToString(); } 
                        }

                        if(result.MaterialMasterShortTextsDetails.MaterialMasterShortTexts != null)
                        {
                            materialdesc = result.MaterialMasterShortTextsDetails.MaterialMasterShortTexts.MaterialDescription.ToString();
                        }
                        r.Data = materialdesc + " " + legacycode;
                    }
                    catch(Exception ex)
                    {
                        string issue = ex.ToString();
                        r.Data = "Problem";
                    }
                }
            }

            foreach (RawInterfaceData r in dto.updates)
            {
                if (r.Data != null)
                {
                    try
                    {
                        Material_Master_Data result;
                        var serializer = new XmlSerializer(typeof(Material_Master_Data));

                        using (var reader = new StringReader(r.Data))
                        {
                            result = (Material_Master_Data)serializer.Deserialize(reader);
                        }
                        string materialdesc = "No Material Description";
                        string legacycode = "No Legacy Code";

                        if (result.MaterialMasterGeneralData != null)
                        {
                            if (result.MaterialMasterGeneralData.LegacyCode != null) { legacycode = result.MaterialMasterGeneralData.LegacyCode.ToString(); }
                        }

                        if (result.MaterialMasterShortTextsDetails.MaterialMasterShortTexts != null)
                        {
                            materialdesc = result.MaterialMasterShortTextsDetails.MaterialMasterShortTexts.MaterialDescription.ToString();
                        }
                        r.Data = materialdesc + " " + legacycode;
                    }
                    catch (Exception ex)
                    {
                        string issue = ex.ToString();
                        r.Data = "Problem";
                    }
                }
            }


            return dto;
        }




        public dtoRawInterfaceOverallSummary getSummaryData()
        {
            dtoRawInterfaceOverallSummary dto = new dtoRawInterfaceOverallSummary();
            string sql = @"DECLARE @dt DATETIME
SET @dt = DATEADD(day,-8,getDate()) 

SELECT B.Cat, Action as Act, iDay as Span, count(B.[Id]) as Tot
FROM
(
SELECT TOP 10000
CASE 
WHEN CHARINDEX('Communication', ServiceID) > 0 THEN 'Communication'
WHEN CHARINDEX('Customer', ServiceID) > 0 THEN 'Customer'
WHEN CHARINDEX('Partner', ServiceID) > 0 THEN 'Partner'
WHEN CHARINDEX('Material', ServiceID) > 0 THEN 'Material'
WHEN CHARINDEX('CMIR', ServiceID) > 0 THEN 'CMIR'
ELSE 'Unknown' END as Cat, abs(DateDiff(day,getDate(),J.Created)) as iDay, Action, ID
FROM [jbsmstr].[scoMaster].[RawInterfaceData] J (NOLOCK) WHERE J.Created >= @dt
) B GROUP BY B.Cat, B.[Action], B.iDay ORDER BY B.iDay, B.Cat ASC";
            List<dtoCatSummary> rData = db.Database.SqlQuery<dtoCatSummary>(sql).ToList();
            dto.errorcount = rData.Where(i => i.act == "ERROR").Sum(i => i.tot);
            dto.insertcount = rData.Where(i => i.act == "INSERT").Sum(i => i.tot);
            dto.updatecount = rData.Where(i => i.act == "UPDATE").Sum(i => i.tot);

            dto.insertmax = 0;
            dto.updatemax = 0;
            dto.errormax = 0;

            for (int i = 0; i < 10; i++)
            {
                dtoGraphStat g = new dtoGraphStat();
                DateTime d = DateTime.Now.AddDays(-i);
                g.x = d.DayOfWeek.ToString().Substring(0, 1).ToUpper().ToString();
                int c = rData.Where(j => j.act == "INSERT").Where(j => j.span == i).Sum(j => j.tot);
                if (c > dto.insertmax) { dto.insertmax = c; }
                g.y = c.ToString();
                g.desc = d.ToShortDateString();
                dto.graphInserts.Add(g);
            }

            for (int i = 0; i < 10; i++)
            {
                dtoGraphStat g = new dtoGraphStat();
                DateTime d = DateTime.Now.AddDays(-i);
                g.x = d.DayOfWeek.ToString().Substring(0, 1).ToUpper().ToString();
                int c = rData.Where(j => j.act == "UPDATE").Where(j => j.span == i).Sum(j => j.tot);
                if (c > dto.updatemax) { dto.updatemax = c; }
                g.y = c.ToString();
                g.desc = d.ToShortDateString();
                dto.graphUpdates.Add(g);
            }


            for (int i = 0; i < 10; i++)
            {
                dtoGraphStat g = new dtoGraphStat();
                DateTime d = DateTime.Now.AddDays(-i);
                g.x = d.DayOfWeek.ToString().Substring(0, 1).ToUpper().ToString();
                int c = rData.Where(j => j.act == "ERROR").Where(j => j.span == i).Sum(j => j.tot);
                if (c > dto.errormax) { dto.errormax = c; }
                g.y = c.ToString();
                g.desc = d.ToShortDateString();
                dto.graphErrors.Add(g);
            }

            if ((dto.updatemax + dto.updatemax) <= 10) { dto.updatemax = 10; }
            if ((dto.insertmax + dto.insertmax) <= 10) { dto.insertmax = 10; }
            if ((dto.errormax + dto.errormax) <= 10) { dto.errormax = 10; }

            var gUpdate = rData.Where(o => o.act == "UPDATE").GroupBy(o => o.cat)
                             .Select(g => new { cat = g.Key, tot = g.Sum(i => i.tot) }).ToList();

            foreach (var r in gUpdate.OrderByDescending(v => v.tot))
            {
                dtoCatSummary c = new dtoCatSummary();
                c.cat = r.cat;
                c.tot = r.tot;
                    dto.updatesummary.Add(c);
            }

            var gInsert = rData.Where(o => o.act == "INSERT").GroupBy(o => o.cat)
                             .Select(g => new { cat = g.Key, tot = g.Sum(i => i.tot) }).ToList();

            foreach (var r in gInsert.OrderByDescending(v => v.tot))
            {
                dtoCatSummary c = new dtoCatSummary();
                c.cat = r.cat;
                c.tot = r.tot;
                dto.insertsummary.Add(c);
            }

            var gError = rData.Where(o => o.act == "ERROR").GroupBy(o => o.cat)
                            .Select(g => new { cat = g.Key, tot = g.Sum(i => i.tot) }).ToList();

            foreach (var r in gError.OrderByDescending(v => v.tot))
            {
                dtoCatSummary c = new dtoCatSummary();
                c.cat = r.cat;
                c.tot = r.tot;
                dto.errorsummary.Add(c);
            }
            return dto;
        }


    }
}