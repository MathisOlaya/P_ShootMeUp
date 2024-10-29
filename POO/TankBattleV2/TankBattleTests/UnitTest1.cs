
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using TankBattleV2;

namespace TankBattleTests
{
    [TestClass]
    public class UnitTest1
    {
        //Cette méthode s'effectue entre chaque test, et permet de reset le contenu, car la liste présente
        // est static, et donc peut pertuber le résultat de test suivant.
        [TestInitialize]
        public void Setup()
        {
            // Réinitialiser
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

            //Calculer le nombre de tank après
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
            GameRoot game = new GameRoot(); //Créer une partie
            int InitialEntityCount = EntityManager.Entities.Count;  //Enregistrer le nombre d'entité avant le lancement

            //////////////////////////////////////////[ Act ]///////////////////////////////////////////
            game.RunOneFrame(); //Permet de lancer le projet durant une image, ça m'évite de devoir tout initialiser manuellement.
            EntityManager.Initialize(1); //Créer les entités

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
            Tank tank = new Tank(new Vector2(50, DefaultPositionY)); //Chaque Tank apparait EN DEHORS de l'écran, puis est sensé avancé jusqu'a la limite (LIMIT_POSITION_Y).

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
            Player player = new Player(Vector2.Zero);   //Sa position nous est égale.

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
            //Attribuer les positions aux dictionnaires, elle le sont pas par défaut pour des raisons techniques, notemment lorsque je dois reset le dico.
            EntityConfig.Tank.SetDefaultSpawnPoints();
            //Recupérer les positions du dictionnaire.
            foreach (var spawnPoint in EntityConfig.Tank.spawnPoints.Keys)
                AllPositions.Add(spawnPoint);

            game.RunOneFrame(); //Simuler le lancement du jeu.

            //Créer le tank
            tank = new Tank(Vector2.Zero);  //Cette position est fausse, car elle sera attribué dans le initialize, du moins c'est ce que nous testons !
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
            //Vérifier que le résultat soit correcte
            Assert.IsTrue(DidHeGetThePositionFromTheDictionnary);
            
        }
        [TestMethod]
        public void TestThatAnEntityCanBeRemove()
        {
            //////////////////////////////////////////[ Arrange ]///////////////////////////////////////////
            Tank tank;  //tank
            int EntityCount = 0; //Par défaut il n'y a pas d'entité

            //////////////////////////////////////////[ Act ]///////////////////////////////////////////
            //Créer le tank
            tank = new Tank(Vector2.Zero);
            EntityManager.Add(tank);

            //Mettre à jour le nombre d'entité
            EntityCount = EntityManager.Entities.Count;

            //Puis supprimer le tank
            EntityManager.Remove(tank);

            //////////////////////////////////////////[ Assert ]///////////////////////////////////////////
            //Vérifer le nombre d'entité calculé après l'ajout d'une entité n'est pas égal au nombre d'entité apres en avoir retiré une.
            Assert.IsTrue(EntityCount != EntityManager.Entities.Count);
        }
    }
}

//////////////////////////////////////////[ Arrange ]///////////////////////////////////////////
//////////////////////////////////////////[ Act ]///////////////////////////////////////////
//////////////////////////////////////////[ Assert ]///////////////////////////////////////////