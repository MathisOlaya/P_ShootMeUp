using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TankBattleV2
{
    public static class Visuals
    {
        public static void LoadTextures(ContentManager content)
        {
            //Charger la texture du joueur.
            EntityConfig.Player.Texture = content.Load<Texture2D>("Textures/Player/player");

            //Charger la munition du joueur
            EntityConfig.Bullet.Texture = content.Load<Texture2D>("Textures/Bullets/PlayerBullet");

            //Charger l'image de la capacité des munitions 
            EntityConfig.Bullet.IconTexture = content.Load<Texture2D>("Textures/Bullets/icon-bullet");

            //Charger la texture du tank
            EntityConfig.Tank.Texture = content.Load<Texture2D>("Textures/Tank/ex");

            //Charger les textures de la vie du tank
            EntityConfig.Tank.LifeBarTextures[0] = content.Load<Texture2D>("Textures/HealthPoint/Tank/dead");
            EntityConfig.Tank.LifeBarTextures[1] = content.Load<Texture2D>("Textures/HealthPoint/Tank/mid");
            EntityConfig.Tank.LifeBarTextures[2] = content.Load<Texture2D>("Textures/HealthPoint/Tank/full");

            //Charger le missile.
            EntityConfig.Shell.Texture = content.Load<Texture2D>("Textures/Bullets/TankBullet");

            //Charger la texture de l'icone de vie du joueur 
            EntityConfig.Player.HealthPointTexture = content.Load<Texture2D>("Textures/HealthPoint/Player/player-healthpoint");

            //Charger la texture de la protection 
            EntityConfig.Protection.Texture = content.Load<Texture2D>("Textures/Protection/sand-bag");
        }
    }
}
