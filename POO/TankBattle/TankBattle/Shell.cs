﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattle
{
    public class Shell : DrawableGameComponent
    {
        private Texture2D texture; //Texture de la munition.
        private SpriteFont spriteFont;
        private SpriteBatch _spriteBatch;

        //Variables 
        private Vector2 _ShellPosition;
        private Vector2 _Direction;
        private const float _SPEED = 10;
        private Vector2 _Velocity;

        public Shell(Game game, Vector2 TankPosition) : base(game)
        {
            /*Calculer la position du canon, car la position du tank se trouve au centre du sprite, alors que 
             * la sortie du canon est positionnée ailleurs. Cependant il n'existe aucun calcul me permettant de trouver la position. 
            J'ai donc du calculer à taton*/
            _ShellPosition = new Vector2(TankPosition.X, TankPosition.Y +85);
        }
        public override void Initialize()
        {
            base.Initialize();

            //Calculer la direction du joueur depuis le tank : posJoueur - posTank .Normaliser afin d'avoir une valeur entre 0 et 1.
            _Direction = (GameRoot.Player.Position - _ShellPosition);
            _Direction.Normalize();
        }
        protected override void LoadContent()
        {
            base.LoadContent();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            //Charger le sprite.
            texture = Game.Content.Load<Texture2D>("TankBullet");

        }
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            //Calculer la vélocité 
            _Velocity = _Direction * _SPEED;
            _ShellPosition += _Velocity;

            //Regarder si la "shell" rentre en collision avec le joueur.
            if(GameRoot.Player != null)
                CheckCollisions();
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            _spriteBatch.Begin();
            _spriteBatch.Draw(texture, _ShellPosition, null, Color.White, (float)(MathF.Atan2(_Direction.Y, _Direction.X) - Math.PI / 2), new Vector2(texture.Width / 2, texture.Height / 2), 0.15f, SpriteEffects.None, 0f);
            _spriteBatch.End();
        }
        
        private void CheckCollisions()
        {
            //Créer une liste qui contiendra les protections a supprimer, car je ne peux pas le supprimer directement, sinon cela produit une erreur
            List<Protection> ProtectionToRemove = new List<Protection>();


            if (CollisionHelpers.IsCollidingWith(_ShellPosition, GameRoot.Player))
            {
                //Supprimer la munition
                Game.Components.Remove(this);

                //Retirer une vie au joueur
                GameRoot.Player.HealthPoint -= 1;

                //Si il n'a plus de vie, le supprimer
                if (GameRoot.Player.HealthPoint == 0)
                {
                    //Supprimer le joueur
                    Game.Components.Remove(GameRoot.Player);
                    GameRoot.Player = null;
                }
            }

            //Checker les collisions avec les protections
            foreach(Protection protection in GameRoot.Protections)
            {
                if(CollisionHelpers.IsCollidingWith(_ShellPosition, protection))
                {
                    //Supprimer la shell
                    Game.Components.Remove(this);

                    //Retirer une vie à la protection
                    protection.HealthPoint -= 1;

                    if (protection.HealthPoint < 1)
                    {
                        //Supprimer la protection
                        Game.Components.Remove(protection);

                        //Ajouter à la liste de suppression
                        ProtectionToRemove.Add(protection);
                    }
                }
            }
            foreach (Protection NoLifeProtection in ProtectionToRemove)
            {
                GameRoot.Protections.Remove(NoLifeProtection);
            }
        }
    }
}
