//using microsoft.aspnetcore.mvc;
//using microsoft.aspnetcore.server.kestrel.core.internal.http;
//using microsoft.extensions.logging;
//using POC.bff.web.services.v1.configurations;
//using POC.bff.web.swagger;
//using swashbuckle.aspnetcore.examples;
//using System; using POC.Shared.Responses;
//using system.net;
//using system.threading.tasks;

//namespace POC.bff.web.controllers.v1
//{
//    [route("api/v1/powers")]
//    public class powercontroller : basecontroller
//    {
//        private readonly iconfigurationservice _configurationservice;

//        public powercontroller(ilogger<powercontroller> logger, IResponseService responseService, iconfigurationservice configurationservice) : base(logger, responseService,)
//        {
//            _configurationservice = configurationservice;
//        }

//        /// <summary>
//        ///     find powers
//        /// </summary>
//        /// <returns>list of power</returns>
//        [httpget]
//        [swaggerresponseexample((int)httpstatuscode.ok, typeof(responsesuccessexample))]
//        [swaggerresponseexample((int)httpstatuscode.notfound, typeof(responsefail404example))]
//        public async task<iactionresult> findpower()
//        {
//            return await safeexecuteasync(async () => await _configurationservice.findpowers(), httpmethod.get);
//        }

//        /// <summary>
//        ///     get power by identifier
//        /// </summary>
//        /// <param name="id">power identifier</param>
//        /// <returns>power</returns>
//        [httpget("{id}")]
//        [swaggerresponseexample((int)httpstatuscode.ok, typeof(responsesuccessexample))]
//        [swaggerresponseexample((int)httpstatuscode.notfound, typeof(responsefail404example))]
//        public async task<iactionresult> getpower(guid id)
//        {
//            return await safeexecuteasync(async () => await _configurationservice.getpower(id), httpmethod.get);
//        }

//        /// <summary>
//        ///     find grouped powers
//        /// </summary>
//        /// <returns>list of power</returns>
//        [httpget("grouped")]
//        [swaggerresponseexample((int)httpstatuscode.ok, typeof(responsesuccessexample))]
//        [swaggerresponseexample((int)httpstatuscode.notfound, typeof(responsefail404example))]
//        public async task<iactionresult> findgroupedpower()
//        {
//            return await safeexecuteasync(async () => await _configurationservice.findgroupedpowers(), httpmethod.get);
//        }
//    }
//}

