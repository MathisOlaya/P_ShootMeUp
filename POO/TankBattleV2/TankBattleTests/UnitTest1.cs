
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TankBattleV2;

namespace TankBattleTests
{
    [TestClass]
    public class UnitTest1
    {
        //Cette m�thode s'effectue entre chaque test, et permet de reset le contenu, car la liste pr�sente
        // est static, et donc peut pertuber le r�sultat de test suivant.
        [TestInitialize]
        public void Setup()
        {
            // R�initialiser
            EntityManager.Entities.Clear();
        }

        [TestMethod]
        public void TankAreCreateWhenPlayerClickStart()
        {
            //////////////////////////////////////////[ Arrange ]///////////////////////////////////////////
            int TankCountAtStart = EntityManager.Entities.Count;    //Nombre de tank au lancement
            int TankCountAfterLaunching = 0; //Default 0
            GameRoot game = new GameRoot(); //Instance de la partie

            //////////////////////////////////////////[ Act ]///////////////////////////////////////////
            game.RunOneFrame(); //Lancer pendant une frame
            GameRoot.menu.OnClick(TankBattleV2.Action.Start);   //Simuler le fait que le joueur appuie sur Start

            //Calculer le nombre de tank apr�s
            foreach (Entity entity in EntityManager.Entities)
            {
                if (entity is Tank)
                {
                    TankCountAfterLaunching += 1;
                }
            }

            //////////////////////////////////////////[ Assert ]///////////////////////////////////////////
            Assert.IsTrue(TankCountAfterLaunching > TankCountAtStart);
        }
        [TestMethod]
        public void TestEntityManagerInitialiteMethodAddEntities()
        {
            //////////////////////////////////////////[ Arrange ]///////////////////////////////////////////
            GameRoot game = new GameRoot(); //Cr�er une partie
            int InitialEntityCount = EntityManager.Entities.Count;  //Enregistrer le nombre d'entit� avant le lancement

            //////////////////////////////////////////[ Act ]///////////////////////////////////////////
            game.RunOneFrame(); //Permet de lancer le projet durant une image, �a m'�vite de devoir tout initialiser manuellement.
            EntityManager.Initialize(1); //Cr�er les entit�s

            //////////////////////////////////////////[ Assert ]///////////////////////////////////////////
            Assert.IsTrue(EntityManager.Entities.Count > InitialEntityCount);
        }
        [TestMethod]
        public void TestTankIsMovingWhenHeSCreated()
        {
            //////////////////////////////////////////[ Arrange ]///////////////////////////////////////////
            GameRoot game = new GameRoot();
            int DefaultPositionY = -200;
            GameTime gameTime = new GameTime(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));

            //////////////////////////////////////////[ Act ]///////////////////////////////////////////
            game.RunOneFrame();
            Tank tank = new Tank(new Vector2(50, DefaultPositionY)); //Chaque Tank apparait EN DEHORS de l'�cran, puis est sens� avanc� jusqu'a la limite (LIMIT_POSITION_Y).

            for (int i = 0; i < 100; i++) // Simuler 100 frames
            {
                tank.Update(gameTime);
            }

            //////////////////////////////////////////[ Assert ]///////////////////////////////////////////
            Assert.IsTrue(tank.Position.Y > DefaultPositionY);
        }
        [TestMethod]
        public void TestThatThePlayerHasASpeedAfterCreation()
        {
            //////////////////////////////////////////[ Arrange ]///////////////////////////////////////////
            GameRoot game = new GameRoot();

            //////////////////////////////////////////[ Act ]///////////////////////////////////////////
            //Create player
            Player player = new Player(Vector2.Zero);   //Sa position nous est �gale.

            //////////////////////////////////////////[ Assert ]///////////////////////////////////////////
            Assert.IsNotNull(player.Speed);
        }
        [TestMethod]
        public void TestThatTankGetAPositionFromTheDictionnary()
        {
            //////////////////////////////////////////[ Arrange ]///////////////////////////////////////////
            GameRoot game = new GameRoot();
            List<Vector2> AllPositions = new List<Vector2>();   //Liste contenant toutes les positions
            bool DidHeGetThePositionFromTheDictionnary = false;
            Tank tank; //tank.

            //////////////////////////////////////////[ Act ]///////////////////////////////////////////
            //Attribuer les positions aux dictionnaires, elle le sont pas par d�faut pour des raisons techniques, notemment lorsque je dois reset le dico.
            EntityConfig.Tank.SetDefaultSpawnPoints();
            //Recup�rer les positions du dictionnaire.
            foreach (var spawnPoint in EntityConfig.Tank.spawnPoints.Keys)
                AllPositions.Add(spawnPoint);

            game.RunOneFrame(); //Simuler le lancement du jeu.

            //Cr�er le tank
            tank = new Tank(Vector2.Zero);  //Cette position est fausse, car elle sera attribu� dans le initialize, du moins c'est ce que nous testons !
            tank.Initialize();  //Initialiser le tank.

            //Voir si la position X du tank fait parti de la liste des positions X du dico.
            foreach (Vector2 pos in AllPositions)
            {
                if(tank.Position.X == pos.X)
                {
                    DidHeGetThePositionFromTheDictionnary = true;
                }
            }

            //////////////////////////////////////////[ Assert ]///////////////////////////////////////////
            //V�rifier que le r�sultat soit correcte
            Assert.IsTrue(DidHeGetThePositionFromTheDictionnary);
            
        }
        [TestMethod]
        public void TestThatAnEntityCanBeRemove()
        {
            //////////////////////////////////////////[ Arrange ]///////////////////////////////////////////
            Tank tank;  //tank
            int EntityCount = 0; //Par d�faut il n'y a pas d'entit�

            //////////////////////////////////////////[ Act ]///////////////////////////////////////////
            //Cr�er le tank
            tank = new Tank(Vector2.Zero);
            EntityManager.Add(tank);

            //Mettre � jour le nombre d'entit�
            EntityCount = EntityManager.Entities.Count;

            //Puis supprimer le tank
            EntityManager.Remove(tank);

            //////////////////////////////////////////[ Assert ]///////////////////////////////////////////
            //V�rifer le nombre d'entit� calcul� apr�s l'ajout d'une entit� n'est pas �gal au nombre d'entit� apres en avoir retir� une.
            Assert.IsTrue(EntityCount != EntityManager.Entities.Count);
        }
    }
}

//////////////////////////////////////////[ Arrange ]///////////////////////////////////////////
//////////////////////////////////////////[ Act ]///////////////////////////////////////////
//////////////////////////////////////////[ Assert ]///////////////////////////////////////////