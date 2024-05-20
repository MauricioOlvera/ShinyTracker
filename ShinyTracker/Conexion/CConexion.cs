using System;
using System.Collections.Generic;
using System.Text;
using Firebase.Database;
using Firebase.Storage;

namespace ShinyTracker.Conexion
{
    public class CConexion
    {
        public static FirebaseClient firebase = new FirebaseClient("https://shinytracker-aa356-default-rtdb.firebaseio.com/");

        public static FirebaseStorage firebaseStorage = new FirebaseStorage("shinytracker-aa356.appspot.com");
    }
}
