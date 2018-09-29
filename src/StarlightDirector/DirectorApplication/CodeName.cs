using System;
using JetBrains.Annotations;

namespace OpenCGSS.StarlightDirector.DirectorApplication {
    public static class CodeName {

        [NotNull]
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
                    }
                    break;
                case 1:
                    if (minor < V1List.Length) {
                        return V1List[minor];
                    }
                    break;
            }

            return "Chihiro";
        }

        [NotNull, ItemNotNull]
        private static readonly string[] V1List = {
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
            "Yui", // Until May 2017
            "Sachiko",
            "Nina",
            "Kaede",
            "Yumi",
            "Yuuki",
            "Ryou",
            "Yoshino",
            "Hiromi", // Until September 2018
        };

    }
}
