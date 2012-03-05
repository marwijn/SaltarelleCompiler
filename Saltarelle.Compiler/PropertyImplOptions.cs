using System;

namespace Saltarelle.Compiler {
    public class PropertyImplOptions {
        public enum ImplType {
            /// <summary>
            /// The property is implemented as a get/set method pair, perhaps with a backing field. All of the GetMethodName, SetMethodName and FieldName are valid (but the FieldName might be null if no backing field is needed).
            /// </summary>
            GetAndSetMethods,
            /// <summary>
            /// The property is implemented as a simple field. Only the FieldName is valid.
            /// </summary>
            Field,
            /// <summary>
            /// The property is implemented as a native indexer. It must be an indexer property with one argument.
            /// </summary>
            NativeIndexer,
            /// <summary>
            /// The property is not usable from script. No code is generated for it, and any usages of it will give an error.
            /// </summary>
            NotUsableFromScript,
        }

        /// <summary>
        /// Implementation type.
        /// </summary>
        public ImplType Type { get; private set; }

        private string _fieldName;
        private bool _isFieldStatic;
        private MethodImplOptions _getMethod;
        private MethodImplOptions _setMethod;

        public MethodImplOptions GetMethod {
            get {
                if (Type != ImplType.GetAndSetMethods)
                    throw new InvalidOperationException();
                return _getMethod;
            }
        }

        public MethodImplOptions SetMethod {
            get {
                if (Type != ImplType.GetAndSetMethods)
                    throw new InvalidOperationException();
                return _setMethod;
            }
        }

        public string FieldName {
            get {
                if (Type != ImplType.Field)
                    throw new InvalidOperationException();
                return _fieldName;
            }
        }

        public bool IsFieldStatic {
            get {
                if (Type != ImplType.Field)
                    throw new InvalidOperationException();
                return _isFieldStatic;
            }
        }

        public static PropertyImplOptions GetAndSetMethods(MethodImplOptions getMethod, MethodImplOptions setMethod) {
            return new PropertyImplOptions { Type = ImplType.GetAndSetMethods, _getMethod = getMethod, _setMethod = setMethod };
        }

        public static PropertyImplOptions InstanceField(string fieldName) {
            return new PropertyImplOptions { Type = ImplType.Field, _fieldName = fieldName, _isFieldStatic = false };
        }

        public static PropertyImplOptions StaticField(string fieldName) {
            return new PropertyImplOptions { Type = ImplType.Field, _fieldName = fieldName, _isFieldStatic = true };
        }

        public static PropertyImplOptions NativeIndexer() {
            return new PropertyImplOptions { Type = ImplType.NativeIndexer };
        }

        public static PropertyImplOptions NotUsableFromScript() {
            return new PropertyImplOptions { Type = ImplType.NotUsableFromScript };
        }
    }
}