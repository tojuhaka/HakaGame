using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace HakaGame
{
    public class NPC : AnimatedSprite
    {
        #region Attributes & Properties
        DialogComponent dialog;
        bool isTalking;
        float speakingRadius;

        public bool IsTalking
        {
            set { isTalking = value; }
            get { return isTalking; }
        }

        public float SpeakingRadius
        {
            get { return speakingRadius; }
        }
        #endregion
        #region Constructors
        public NPC(Texture2D texture, Dictionary<AnimationKey, Animation> animations, IEContentManager content) : 
            base(texture, animations)
        {
            speakingRadius = 80f;
            dialog = new DialogComponent(content);
            dialog.Hide();
        }
        #endregion
        #region Methods
        public override void Update()
        {
            base.Update();
            if (dialog.Enabled)
            {
                dialog.Update();
            }
        }

        public override void Draw()
        {
            base.Draw();
            if (dialog.Enabled)
                dialog.Draw();
        }

        public void StartDialog()
        {
            dialog.Show();
            isTalking = true;
        }

        public void StopDialog()
        {
            dialog.Hide();
            isTalking = false;
        }
        #endregion

    }
}
