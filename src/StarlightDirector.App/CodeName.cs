using System;

namespace StarlightDirector.App {
    public static class CodeName {

        public static string GetCodeName(Version directorVersion) {
            var major = directorVersion.Major;
            var minor = directorVersion.Minor;
            switch (major) {
                case 0:
                    switch (minor) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return "Uzuki";
                        case 5:
                            return "Rin";
                        case 6:
                            return "Mio";
                        case 7:
                            return "Miku";
                        default:
                            break;
                    }
                    break;
                case 1:
                    var v1List = new[] {
                        "Riina",
                        "Rika",
                        "Chieri",
                        // OMG they are all gone!
                        "Mayu"
                    };
                    if (minor < v1List.Length) {
                        return v1List[minor];
                    }
                    break;
                default:
                    break;
            }
            return "Mayu";
        }

        private static string GetCodeName_Obsolete(Version directorVersion) {
            var major = directorVersion.Major;
            var minor = directorVersion.Minor;
            switch (major) {
                case 0:
                    switch (minor) {
                        case 0:
                        case 1:
                        case 2:
                        case 3:
                        case 4:
                            return "Uzuki";
                        case 5:
                            return "Rin";
                        case 6:
                            return "Mio";
                        case 7:
                            return "Miku";
                    }
                    break;
                case 1:
                    var v1List = new[] {
                        "Riina",
                        "Rika",
                        "Chieri",
                        "Kirari",
                        "Anzu",
                        "Ranko",
                        "Kanako",
                        "Minami",
                        "Miria",
                        "Anastasia",
                        "Mika",
                        "Miho",
                        "Mizuki",
                        "Airi",
                        "Mayu",
                        "Fumika",
                        "Natsuki",
                        "Sae",
                        "Kanade",
                        "Aiko",
                        "Nana",
                        "Nao",
                        "Akane",
                        "Frederica",
                        "Karen",
                        "Syoko",
                        "Shiki",
                        "Koume",
                        "Sanae",
                        "Yuka",
                        "Syuko",
                        "Yuki",
                        "Momoka",
                        "Arisu",
                        "Yuko",
                        "Kyoko",
                        "Asuka",
                        "Yui"
                    };
                    if (minor < v1List.Length) {
                        return v1List[minor];
                    }
                    break;
            }
            return "Chihiro";
        }

    }
}
