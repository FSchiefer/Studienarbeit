using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace StudienarbeitsProjekt {
    class FileHandler {

        public const String rootDir = @"C:\Studiengaenge\";

        private String dokumentPfad = null;
        private String type; // Typ in Kleinbuchstaben

        public FileHandler (String dokumentPfad) {
            this.dokumentPfad = dokumentPfad;
            this.type = dokumentPfad.Substring( dokumentPfad.LastIndexOf( '.' ) + 1, dokumentPfad.Length - dokumentPfad.LastIndexOf( '.' ) - 1 ).ToLower();
        }

        public String getDokumentPfad() {
            return dokumentPfad;
        }

        public FileHandler() {
        }

        // Ausgabe des Ordnernamens des Dokuments
        public String getFolderName() {
            int beginDirectoryName = dokumentPfad.LastIndexOf( '\\' ) + 1;

            return dokumentPfad.Substring( beginDirectoryName, dokumentPfad.Length - beginDirectoryName );
        }

        // Ausgabe des Dateityps einer Datei
        public String getType() {
            return type;
        }


        // Ausgabe des Dateinamens des Dokuments
        public String titleViewer() {
            String name = String.Empty;
            int beginFileName = dokumentPfad.LastIndexOf( '\\' ) + 1;
            if (dokumentPfad.Contains( '.' )) {
                name = dokumentPfad.Substring( beginFileName, dokumentPfad.LastIndexOf( '.' ) - beginFileName );
            }
            return name;
        }

        # region checkFileType
        public Boolean isValidFileType() {
            return isValidVideoType() || isValidImageType() || isValidDocType();
        }

        public Boolean isValidVideoType() {
            return type.Equals( "wmv" )  || type.Equals( "mp4" ) || type.Equals( "avi" ) || type.Equals( "mpg" ) || type.Equals( "mts" );
        }

        public Boolean isValidImageType() {
            return type.Equals("jpg");
        }

        public Boolean isValidDocType() {
            return type.Equals( "xps" );
        }
        # endregion

        # region getFilePaths

        public static String[] getAllFiles( String fileChooser ) {
            return getVideoFiles( fileChooser ).Concat(
                   getImageFiles( fileChooser ) ).Concat(
                   getDocFiles( fileChooser ) ).ToArray();
        }

        public static String[] getVideoFiles( String fileChooser ) {
            String baseDir = rootDir + fileChooser;

            // da leider keine regulären Ausdrücke unterstützt werden, muss es etwas umständlich gemacht werden:
            String[] dataPath = Directory.GetFiles( baseDir, "*.wmv" ).Concat(
                                Directory.GetFiles( baseDir, "*.mp4" ) ).Concat(
                                Directory.GetFiles( baseDir, "*.mpg" ) ).Concat(
                                Directory.GetFiles( baseDir, "*.avi" ) ).Concat(
                                Directory.GetFiles( baseDir, "*.MTS" ) ).ToArray();
            return dataPath;
        }

        public static String[] getImageFiles( String fileChooser ) {

           return Directory.GetFiles( rootDir + fileChooser, "*.jpg" );
         }

        public static String[] getDocFiles( String fileChooser ) {
        
            return Directory.GetFiles( rootDir + fileChooser, "*.xps" );
        }

        // Wählt die Ordner der einzelnen Sammlungen aus
        public static String[] getCollections( String fileChooser ) {
            String collectionPath = rootDir + fileChooser + "\\collections\\";

            if (Directory.Exists( collectionPath ))
                return Directory.GetDirectories( collectionPath, "*", System.IO.SearchOption.TopDirectoryOnly );
            else
                return null;
        }

        # endregion

        #region Systemimages 

        public static String getImage(String imgName) {
            return rootDir + imgName+".jpg";
        }

        public static String getPlayImage() {
            return rootDir + "Play.jpg";
        }


        public static String getStopImage() {
            return rootDir + "Stop.jpg";
        }


        public static String getPauseImage() {
            return rootDir + "Pause.jpg";
        }


        public static String getMainscatterImage() {
            return rootDir + "DHBW.jpg";
        }

        #endregion
    }
}
