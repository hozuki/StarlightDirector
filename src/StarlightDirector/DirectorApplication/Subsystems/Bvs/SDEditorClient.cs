using System;
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

            var param0 = new GeneralSimInitializeRequestParams {
                SupportedFormats = _supportedScoreFileFormats
            };

            var @params = new object[] {
                param0
            };

            var rpcResult = await SendRequestAsync(serverUri, CommonProtocolMethodNames.General_SimInitialize, @params, null);

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

        internal async void SendReloadRequest([NotNull] EditReloadRequestParams param0) {
            var serverUri = _communication.SimulatorServerUri;

            if (serverUri == null) {
                return;
            }

            var @params = new object[] {
                param0
            };

            var result = await SendRequestAsync(serverUri, CommonProtocolMethodNames.Edit_Reload, @params, null);

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
            return SendRequestWithEmptyBody(CommonProtocolMethodNames.Preview_Play, null);
        }

        internal Task SendPauseRequest() {
            return SendRequestWithEmptyBody(CommonProtocolMethodNames.Preview_Pause, null);
        }

        internal Task SendStopRequest() {
            return SendRequestWithEmptyBody(CommonProtocolMethodNames.Preview_Stop, null);
        }

        private Task SendRequestWithEmptyBody([NotNull] string methodName, [CanBeNull] string id) {
            var serverUri = _communication.SimulatorServerUri;

            if (serverUri == null) {
                return Task.FromResult(0);
            }

            return SendRequestAsync(serverUri, methodName, null, id);
        }

        private Task SendRequestWithEmptyBody([NotNull] string methodName, int id) {
            var serverUri = _communication.SimulatorServerUri;

            if (serverUri == null) {
                return Task.FromResult(0);
            }

            return SendRequestAsync(serverUri, methodName, null, id);
        }

        private Task SendNotificationWithEmptyBody([NotNull] string methodName) {
            var serverUri = _communication.SimulatorServerUri;

            if (serverUri == null) {
                return Task.FromResult(0);
            }

            return SendNotificationAsync(serverUri, methodName);
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
