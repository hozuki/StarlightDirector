using System;
using System.IO;
using JetBrains.Annotations;

namespace OpenCGSS.Director.Core {
    public static class Guard {

        [ContractAnnotation("value:null => halt")]
        public static void NotNull<T>([CanBeNull] T value, [NotNull, InvokerParameterName] string paramName) {
            if (paramName == null) {
                throw new ArgumentNullException(nameof(paramName));
            }

            if (ReferenceEquals(value, null)) {
                throw new NullReferenceException($"\"{paramName}\" should not be null.");
            }
        }

        [ContractAnnotation("value:null => halt")]
        public static void NotNull<T>([CanBeNull] T value) {
            if (ReferenceEquals(value, null)) {
                throw new NullReferenceException();
            }
        }

        [ContractAnnotation("value:null => halt")]
        public static void NotNullOrEmpty([CanBeNull] string value, [NotNull, InvokerParameterName] string name) {
            if (name == null) {
                throw new ArgumentNullException(nameof(name));
            }

            if (string.IsNullOrEmpty(value)) {
                throw new ArgumentException($"\"{name}\" should not be null or empty.");
            }
        }

        [ContractAnnotation("value:null => halt")]
        public static void ArgumentNotNull<T>([CanBeNull] T value, [NotNull, InvokerParameterName] string paramName) {
            if (paramName == null) {
                throw new ArgumentNullException(nameof(paramName));
            }

            if (ReferenceEquals(value, null)) {
                throw new ArgumentNullException(paramName);
            }
        }

        [ContractAnnotation("value:null => halt")]
        public static void ArgumentNotNull<T>([CanBeNull] T value) {
            if (ReferenceEquals(value, null)) {
                throw new ArgumentNullException();
            }
        }

        public static void FileExists([CanBeNull] string path) {
            if (path == null) {
                throw new ArgumentNullException(nameof(path));
            }

            if (!File.Exists(path)) {
                throw new FileNotFoundException("Requested file is not found.", path);
            }
        }

    }
}
