using System;
using System.Diagnostics;
using JetBrains.Annotations;
using OpenCGSS.StarlightDirector.DirectorApplication.Subsystems.Bvs.Models;
using OpenMLTD.Piyopiyo.Extensions;
using OpenMLTD.Piyopiyo.Net.Contributed;
using OpenMLTD.Piyopiyo.Net.JsonRpc;

namespace OpenCGSS.StarlightDirector.DirectorApplication.Subsystems.Bvs {
    internal sealed class SDEditorServer : EditorServer {

        internal SDEditorServer([NotNull] SDCommunication communication) {
            _communication = communication;
        }

        protected override void OnGeneralSimLaunched(object sender, JsonRpcMethodEventArgs e) {
            if (JsonRpcHelper.IsRequestValid(e.ParsedRequestObject, out string errorMessage)) {
                var requestObject = JsonRpcHelper.TranslateAsRequest(e.ParsedRequestObject);
                var param0 = requestObject.Params[0];

                Debug.Assert(param0 != null, nameof(param0) + " != null");

                var param0Object = param0.ToObject<GeneralSimLaunchedNotificationParams>();

                var simulatorServerUri = new Uri(param0Object.SimulatorServerUri);

                _communication.SimulatorLifeCycleStage = LifeCycleStage.Launched;
                _communication.SimulatorServerUri = simulatorServerUri;

                e.Context.RpcOk();

                _communication.Client.SendSimInitializeRequest();
            } else {
                Debug.Print(errorMessage);
                e.Context.RpcError(JsonRpcErrorCodes.InvalidRequest, errorMessage);
            }
        }

        protected override void OnGeneralSimExited(object sender, JsonRpcMethodEventArgs e) {
            if (JsonRpcHelper.IsRequestValid(e.ParsedRequestObject, out string errorMessage)) {
                _communication.SimulatorLifeCycleStage = LifeCycleStage.Exited;
                _communication.SimulatorServerUri = null;
                e.Context.RpcOk();
            } else {
                Debug.Print(errorMessage);
                e.Context.RpcError(JsonRpcErrorCodes.InvalidRequest, errorMessage);
            }
        }

        protected override void OnPreviewPlaying(object sender, JsonRpcMethodEventArgs e) {
            if (JsonRpcHelper.IsRequestValid(e.ParsedRequestObject, out string errorMessage)) {
                e.Context.RpcOk();
            } else {
                Debug.Print(errorMessage);
                e.Context.RpcError(JsonRpcErrorCodes.InvalidRequest, errorMessage);
            }
        }

        protected override void OnPreviewTick(object sender, JsonRpcMethodEventArgs e) {
            if (JsonRpcHelper.IsRequestValid(e.ParsedRequestObject, out string errorMessage)) {
                e.Context.RpcOk();
            } else {
                Debug.Print(errorMessage);
                e.Context.RpcError(JsonRpcErrorCodes.InvalidRequest, errorMessage);
            }
        }

        protected override void OnPreviewPaused(object sender, JsonRpcMethodEventArgs e) {
            if (JsonRpcHelper.IsRequestValid(e.ParsedRequestObject, out string errorMessage)) {
                e.Context.RpcOk();
            } else {
                Debug.Print(errorMessage);
                e.Context.RpcError(JsonRpcErrorCodes.InvalidRequest, errorMessage);
            }
        }

        protected override void OnPreviewStopped(object sender, JsonRpcMethodEventArgs e) {
            if (JsonRpcHelper.IsRequestValid(e.ParsedRequestObject, out string errorMessage)) {
                e.Context.RpcOk();
            } else {
                Debug.Print(errorMessage);
                e.Context.RpcError(JsonRpcErrorCodes.InvalidRequest, errorMessage);
            }
        }

        protected override void OnPreviewSought(object sender, JsonRpcMethodEventArgs e) {
            if (JsonRpcHelper.IsRequestValid(e.ParsedRequestObject, out string errorMessage)) {
                e.Context.RpcOk();
            } else {
                Debug.Print(errorMessage);
                e.Context.RpcError(JsonRpcErrorCodes.InvalidRequest, errorMessage);
            }
        }

        protected override void OnEditReloaded(object sender, JsonRpcMethodEventArgs e) {
            if (JsonRpcHelper.IsRequestValid(e.ParsedRequestObject, out string errorMessage)) {
                _communication.SimulatorLifeCycleStage = LifeCycleStage.Reloaded;
                e.Context.RpcOk();
            } else {
                Debug.Print(errorMessage);
                e.Context.RpcError(JsonRpcErrorCodes.InvalidRequest, errorMessage);
            }
        }

        private readonly SDCommunication _communication;

    }
}
