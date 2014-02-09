using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CloudBackup.file;

namespace CloudBackup.file
{
    /// <summary>
    /// Description résumée pour FileEncryptTest
    /// </summary>
    [TestClass]
    public class FileHelperTest
    {
        private static string SRC_PATH = "c:\\temp";
        private static string TEST_PATH_LONG = "c:\\temp\\temp2\\temp3";
        private static string TEST_PATH_LONG_TRALING = "c:\\temp\\temp2\\temp3\\";

        private static string TEST_PATH_CRYPT = "c:\\temp2\\DiAG54akIt86vl8wEIuy0g==\\kmMUhoGoRY0m9mJ2w1amIw==";


        private static string DST_PATH="c:\\temp2";
        private string KEY_AES = "unitTestForFolderCrypt";

        public FileHelperTest()
        {
            //
            // TODO: ajoutez ici la logique du constructeur
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Obtient ou définit le contexte de test qui fournit
        ///des informations sur la série de tests active ainsi que ses fonctionnalités.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Attributs de tests supplémentaires
        //
        // Vous pouvez utiliser les attributs supplémentaires suivants lorsque vous écrivez vos tests :
        //
        // Utilisez ClassInitialize pour exécuter du code avant d'exécuter le premier test de la classe
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Utilisez ClassCleanup pour exécuter du code une fois que tous les tests d'une classe ont été exécutés
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Utilisez TestInitialize pour exécuter du code avant d'exécuter chaque test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Utilisez TestCleanup pour exécuter du code après que chaque test a été exécuté
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethodEncryptPathNullPath()
        {
            Assert.IsNull(FileHelper.encryptPath(null,"toto","titi","tata"));
        }

        [TestMethod]
        public void TestMethodEncryptPathNullSrcPath()
        {
            Assert.IsNull(FileHelper.encryptPath("toto", null, "titi", "tata"));
        }

        [TestMethod]
        public void TestMethodEncryptPathNullDstPath()
        {
            Assert.IsNull(FileHelper.encryptPath("tutu", "toto",null, "tata"));
        }

        [TestMethod]
        public void TestMethodEncryptPathNullKey()
        {
            Assert.IsNull(FileHelper.encryptPath("tutu", "toto", "titi", null));
        }
        [TestMethod]
        public void TestMethodEncryptBasic()
        {
            Assert.AreNotEqual(FileHelper.translateFilemame(SRC_PATH,DST_PATH,TEST_PATH_LONG+"\\test.jpg"),FileHelper.encryptPath(TEST_PATH_LONG+"\\test.jpg",SRC_PATH,DST_PATH,KEY_AES));
        }

        [TestMethod]
        public void TestMethodEncryptWithTraling()
        {
            Assert.AreEqual(FileHelper.encryptPath(TEST_PATH_LONG,SRC_PATH,DST_PATH,KEY_AES),FileHelper.encryptPath(TEST_PATH_LONG_TRALING,SRC_PATH,DST_PATH,KEY_AES));
        }

        [TestMethod]
        public void testMethodEncryptComplex() {
            string cryptPath = FileHelper.encryptPath(TEST_PATH_LONG, SRC_PATH, DST_PATH, KEY_AES);
            string[] cryptPaths = cryptPath.Replace(DST_PATH,"").Split('\\');
            string[] paths = TEST_PATH_LONG.Replace(SRC_PATH,"").Split('\\');

            Assert.AreEqual(cryptPaths.Length, paths.Length);
            for (int i = 1; i < paths.Length; i++)
            {
                Assert.AreEqual(cryptPaths[i], EncryptionHelper.EncryptString(paths[i], KEY_AES));
            }
        }




        [TestMethod]
        public void TestMethodDecryptPathNullPath()
        {
            Assert.IsNull(FileHelper.decryptPath(null, "toto", "titi", "tata"));
        }

        [TestMethod]
        public void TestMethodDecryptPathNullSrcPath()
        {
            Assert.IsNull(FileHelper.decryptPath("toto", null, "titi", "tata"));
        }

        [TestMethod]
        public void TestMethodDecryptPathNullDstPath()
        {
            Assert.IsNull(FileHelper.decryptPath("tutu", "toto", null, "tata"));
        }

        [TestMethod]
        public void TestMethodDecryptPathNullKey()
        {
            Assert.IsNull(FileHelper.decryptPath("tutu", "toto", "titi", null));
        }
        [TestMethod]
        public void TestMethodDecryptBasic()
        {
            Assert.AreNotEqual(FileHelper.translateFilemame(DST_PATH, SRC_PATH, TEST_PATH_CRYPT), FileHelper.decryptPath(TEST_PATH_CRYPT, DST_PATH, SRC_PATH, KEY_AES));
        }
        
        [TestMethod]
        public void testMethodDecryptComplex()
        {
            string clainPath = FileHelper.decryptPath(TEST_PATH_CRYPT, DST_PATH, SRC_PATH, KEY_AES);
            string[] clainPaths = clainPath.Replace(SRC_PATH, "").Split('\\');
            string[] paths = TEST_PATH_CRYPT.Replace(DST_PATH, "").Split('\\');

            Assert.AreEqual(clainPaths.Length, paths.Length);
            for (int i = 1; i < paths.Length; i++)
            {
                Assert.AreEqual(clainPaths[i], EncryptionHelper.DecryptString(paths[i], KEY_AES));
            }
        }
    }
}
