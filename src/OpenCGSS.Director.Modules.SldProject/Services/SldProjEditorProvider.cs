using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Threading.Tasks;
using Gemini.Framework;
using Gemini.Framework.Services;
using OpenCGSS.Director.Modules.SldProject.ViewModels;

namespace OpenCGSS.Director.Modules.SldProject.Services {
    [Export(typeof(IEditorProvider))]
    public sealed class SldProjEditorProvider : IEditorProvider {

        public bool Handles(string path) {
            var fileInfo = new FileInfo(path);
            var extension = fileInfo.Extension.ToLowerInvariant();

            return Array.Exists(SupportedFileTypes, t => t.FileExtension == extension);
        }

        public IDocument Create() {
            return new SldProjViewModel {
                DisplayName = null
            };
        }

        public async Task New(IDocument document, string name) {
            var persisted = (SldProjViewModel)document;

            await persisted.New(name);
        }

        public async Task Open(IDocument document, string path) {
            var persisted = (SldProjViewModel)document;

            await persisted.Load(path);
        }

        public IEnumerable<EditorFileType> FileTypes => SupportedFileTypes;

        private static readonly EditorFileType[] SupportedFileTypes = {
            new EditorFileType("Starlight Director Project (*.sldproj)", ".sldproj")
        };

    }
}
