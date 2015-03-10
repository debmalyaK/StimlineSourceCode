using Stimline.Data.RunDomain;
using Stimline.Data.RunDomain.Domain;
using Stimline.Data.ProjectsDomain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Xml.Serialization;

namespace ServiceCommon
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class SimulatorWCFService : ISimulatorWCFService
    {
        public bool UpdateSimulationConfiguration(GeneralSimulationConfiguration config)
        {
            bool isUpdated = false;
            try
            {
                XmlSerializer writer = new XmlSerializer(config.GetType());
                string filePath = AppDomain.CurrentDomain.BaseDirectory + "\\GeneralSimulationConfiguration.xml";
                StreamWriter file = new StreamWriter(filePath);
                writer.Serialize(file, config);
                file.Close();
            }
            catch (Exception ex)
            {
                SimulatorException simulatorException = new SimulatorException();
                simulatorException.Title = "Error in SimulatorWCFService.UpdateSimulationConfiguration method";
                simulatorException.ExceptionMessage = ex.Message;
                simulatorException.InnerException = ex.InnerException;
                simulatorException.StackTrace = ex.StackTrace;
                throw new FaultException<SimulatorException>(simulatorException);
            }
            return isUpdated;
        }

        public List<RunPlanInfo> GetAllRunPlans()
        {
            List<RunPlanInfo> allPlanInfo = new List<RunPlanInfo>();
            List<RunPlan> runPlans = new List<RunPlan>();
            List<Project> projects = new List<Project>();
            List<Customer> customers = new List<Customer>();
            List<Well> wells = new List<Well>();
            List<Wellbore> wellbores = new List<Wellbore>();
            try
            {
                using (var db = new RunContext())
                {
                    runPlans.AddRange(db.RunPlans.ToList());
                }
                using(var db= new ProjectsContext())
                {
                    projects.AddRange(db.Projects);
                    customers.AddRange(db.Customers);
                    wells.AddRange(db.Wells);
                    wellbores.AddRange(db.Wellbores);
                }
                foreach(RunPlan plan in runPlans)
                {
                    RunPlanInfo info = new RunPlanInfo();
                    info.Name = plan.Name;
                    info.Id = plan.Id;
                    info.IsPublished = plan.IsPublished;
                    info.PublishedDate = plan.PublishedDate;
                    info.RunTask = plan.RunTask;
                    info.UnitId = plan.UnitId;
                    if (!string.IsNullOrEmpty(plan.CustomerId.ToString()))
                    {
                        info.CustomerId = plan.CustomerId;
                        info.CustomerName = customers.Where(x => x.Id.ToString() == plan.CustomerId.ToString()).Select(x => x.Name).FirstOrDefault();
                    }
                    if (!string.IsNullOrEmpty(plan.ProjectId.ToString()))
                    {
                        info.ProjectId = plan.ProjectId;
                        info.ProjectName = projects.Where(x => x.Id.ToString() == plan.ProjectId.ToString()).Select(x => x.Name).FirstOrDefault();
                    }
                    if (!string.IsNullOrEmpty(plan.WellboreId.ToString()))
                    {
                        info.WellboreId = plan.WellboreId;
                        Guid wellId = wellbores.Where(x => x.Id.ToString() == plan.WellboreId.ToString()).Select(x => x.WellId).FirstOrDefault();
                        info.WellName = wells.Where(x => x.Id.ToString() == wellId.ToString()).Select(x => x.Name).FirstOrDefault();
                    }
                    allPlanInfo.Add(info);
                }
            }
            catch (Exception ex)
            {
                SimulatorException simulatorException = new SimulatorException();
                simulatorException.Title = "Error in SimulatorWCFService.GetAllRunPlans method";
                simulatorException.ExceptionMessage = ex.Message;
                simulatorException.InnerException = ex.InnerException;
                simulatorException.StackTrace = ex.StackTrace;
                throw new FaultException<SimulatorException>(simulatorException);
            }
            return allPlanInfo;
        }

        public bool AddRunPlan(RunPlan runPlan)
        {
            bool isAdded = false;
            try
            {
                using (var db = new RunContext())
                {
                    db.RunPlans.Add(runPlan);
                    db.Flush();
                    isAdded = true;
                }
            }
            catch (Exception ex)
            {
                SimulatorException simulatorException = new SimulatorException();
                simulatorException.Title = "Error in SimulatorWCFService.AddRunPlan method";
                simulatorException.ExceptionMessage = ex.Message;
                simulatorException.InnerException = ex.InnerException;
                simulatorException.StackTrace = ex.StackTrace;
                throw new FaultException<SimulatorException>(simulatorException);
            }
            return isAdded;
        }

        public bool EditRunPlan(RunPlan runPlan)
        {
            bool isUpdated = false;
            try
            {
                using (var db = new RunContext())
                {
                    db.RunPlans.Update(runPlan);
                    db.Flush();
                    isUpdated = true;
                }
            }
            catch (Exception ex)
            {
                SimulatorException simulatorException = new SimulatorException();
                simulatorException.Title = "Error in SimulatorWCFService.EditRunPlan method";
                simulatorException.ExceptionMessage = ex.Message;
                simulatorException.InnerException = ex.InnerException;
                simulatorException.StackTrace = ex.StackTrace;
                throw new FaultException<SimulatorException>(simulatorException);
            }
            return isUpdated;
        }

        public bool DeleteRunPlan(Guid runPlanId)
        {
            bool isDeleted = false;
            try
            {
                using (var db = new RunContext())
                {
                    db.RunPlans.DeleteById(runPlanId);
                    db.Flush();
                    isDeleted = true;
                }
            }
            catch (Exception ex)
            {
                SimulatorException simulatorException = new SimulatorException();
                simulatorException.Title = "Error in SimulatorWCFService.DeleteRunPlan method";
                simulatorException.ExceptionMessage = ex.Message;
                simulatorException.InnerException = ex.InnerException;
                simulatorException.StackTrace = ex.StackTrace;
                throw new FaultException<SimulatorException>(simulatorException);
            }
            return isDeleted;
        }

        public List<RunInfo> GetAllRuns()
        {
            List<RunInfo> runs = new List<RunInfo>();
            try
            {
                using (var db = new RunContext())
                {
                    foreach (Run run in db.Runs.ToList())
                    {
                        RunInfo runInfo = new RunInfo();
                        runInfo.Id = run.Id;
                        runInfo.Comment = run.Comment;
                        runInfo.EndTime = run.EndTime;
                        runInfo.RunNumber = run.RunNumber;
                        runInfo.RunPlanId = run.RunPlanId;
                        runInfo.StartTime = run.StartTime;
                        runInfo.Status = run.Status;
                        runInfo.TimeZone = run.TimeZone;
                        runInfo.Toolstring = run.Toolstring;
                        runs.Add(runInfo);
                    }
                }
            }
            catch (Exception ex)
            {
                SimulatorException simulatorException = new SimulatorException();
                simulatorException.Title = "Error in SimulatorWCFService.GetAllRuns method";
                simulatorException.ExceptionMessage = ex.Message;
                simulatorException.InnerException = ex.InnerException;
                simulatorException.StackTrace = ex.StackTrace;
                throw new FaultException<SimulatorException>(simulatorException);
            }
            return runs;
        }

        public List<Run> GetAllRunsWithSpecifiedRunPlanId(Guid runPlanId)
        {
            List<Run> runs = new List<Run>();
            try
            {
                using (var db = new RunContext())
                {
                    runs.AddRange(db.Runs.ToList().FindAll(x => x.RunPlanId.Equals(runPlanId)));
                }
            }
            catch (Exception ex)
            {
                SimulatorException simulatorException = new SimulatorException();
                simulatorException.Title = "Error in SimulatorWCFService.GetAllRunsWithSpecifiedRunPlanId method";
                simulatorException.ExceptionMessage = ex.Message;
                simulatorException.InnerException = ex.InnerException;
                simulatorException.StackTrace = ex.StackTrace;
                throw new FaultException<SimulatorException>(simulatorException);
            }
            return runs;
        }

        public List<Run> GetAllRunsFilteredByStatus(RunStatusEnum runStatus)
        {
            List<Run> runs = new List<Run>();
            try
            {
                using (var db = new RunContext())
                {
                    runs.AddRange(db.Runs.ToList().FindAll(x => x.Status.Equals(runStatus)));
                }
            }
            catch(Exception ex)
            {
                SimulatorException simulatorException = new SimulatorException();
                simulatorException.Title = "Error in SimulatorWCFService.GetAllRunsFilteredByStatus method";
                simulatorException.ExceptionMessage = ex.Message;
                simulatorException.InnerException = ex.InnerException;
                simulatorException.StackTrace = ex.StackTrace;
                throw new FaultException<SimulatorException>(simulatorException);
            }
            return runs;
        }
    }
}
