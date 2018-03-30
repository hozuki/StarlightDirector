using System.Diagnostics;
using System.Threading.Tasks;
using JetBrains.Annotations;
using OpenCGSS.StarlightDirector.DirectorApplication.Subsystems.Bvs.Models;
using OpenCGSS.StarlightDirector.DirectorApplication.Subsystems.Bvs.Models.Proposals;
using OpenMLTD.Piyopiyo;
using OpenMLTD.Piyopiyo.Extensions;
using OpenMLTD.Piyopiyo.Net.JsonRpc;

namespace OpenCGSS.StarlightDirector.DirectorApplication.Subsystems.Bvs {
    internal sealed class SDEditorClient : JsonRpcClient {

        internal SDEditorClient([NotNull] SDCommunication communication) {
            _communication = communication;
        }

        internal async void SendSimInitializeRequest() {
            var serverUri = _communication.SimulatorServerUri;

            if (serverUri == null) {
                return;
            }

            var @params = new GeneralSimInitializeRequestParams {
                Data = new GeneralSimInitializeRequestParams.GeneralSimInitializeRequestParamsData {
                    SupportedFormats = _supportedScoreFileFormats
                }
            };

            var rpcResult = await CallAsync(serverUri, CommonProtocolMethodNames.General_SimInitialize, @params);

            if (!rpcResult.StatusCode.IsSuccessful()) {
                // TODO: Handle HTTP protocol errors.
                return;
            }

            Debug.Assert(rpcResult.ResponseObject != null, "result.ResponseObject != null");

            if (!JsonRpcHelper.IsResponseValid(rpcResult.ResponseObject)) {
                // TODO: Handle malformed RPC object.
                return;
            }

            if (JsonRpcHelper.IsResponseSuccessful(rpcResult.ResponseObject)) {
                var response = JsonRpcHelper.TranslateAsResponse(rpcResult.ResponseObject);

                Debug.Assert(response.Result != null, "response.Result != null");

                var result = response.GetResult<GeneralSimInitializeResponseResult>();

                Debug.Assert(result != null, nameof(result) + " != null");

                var selectedFormat = result.SelectedFormat;

                if (selectedFormat == null) {
                    // No common format
                } else {
                    // Select it as the common format
                }
            } else {
                var error = JsonRpcHelper.TranslateAsResponse(rpcResult.ResponseObject);
            }
        }

        internal async void SendReloadRequest([NotNull] EditReloadRequestParams @params) {
            var serverUri = _communication.SimulatorServerUri;

            if (serverUri == null) {
                return;
            }

            var result = await CallAsync(serverUri, CommonProtocolMethodNames.Edit_Reload, @params);

            if (!result.StatusCode.IsSuccessful()) {
                // TODO: Handle HTTP protocol errors.
                return;
            }

            Debug.Assert(result.ResponseObject != null, "result.ResponseObject != null");

            if (!JsonRpcHelper.IsResponseValid(result.ResponseObject)) {
                // TODO: Handle malformed RPC object.
                return;
            }

            if (JsonRpcHelper.IsResponseSuccessful(result.ResponseObject)) {
                var response = JsonRpcHelper.TranslateAsResponse(result.ResponseObject);
            } else {
                var error = JsonRpcHelper.TranslateAsResponse(result.ResponseObject);
            }
        }

        internal Task SendEdExitedNotification() {
            return SendNotificationWithEmptyBody(CommonProtocolMethodNames.General_EdExited);
        }

        internal Task SendPlayRequest() {
            return SendNotificationWithEmptyBody(CommonProtocolMethodNames.Preview_Play);
        }

        internal Task SendPauseRequest() {
            return SendNotificationWithEmptyBody(CommonProtocolMethodNames.Preview_Pause);
        }

        internal Task SendStopRequest() {
            return SendNotificationWithEmptyBody(CommonProtocolMethodNames.Preview_Stop);
        }

        private Task SendNotificationWithEmptyBody([NotNull] string methodName) {
            var serverUri = _communication.SimulatorServerUri;

            if (serverUri == null) {
                return Task.FromResult(0);
            }

            return CallAsync(serverUri, methodName);
        }

        private readonly SupportedFormatDescriptor[] _supportedScoreFileFormats = {
            new SupportedFormatDescriptor {
                Game = "deresute",
                FormatId = "sldproj",
                Versions = new [] {
                    "400",
                    "301",
                    "300",
                    "200"
                }
            }
        };

        private readonly SDCommunication _communication;

    }
}
