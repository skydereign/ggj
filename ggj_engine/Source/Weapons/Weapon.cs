using ggj_engine.Source.Weapons.Inputs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ggj_engine.Source.Weapons
{
    public class Weapon
    {
        List<EquipInput> inputs;

        public Weapon()
        {
            inputs = new List<EquipInput>();
            inputs.Add(new EquipMouseInput(EquipMouseInput.Types.Pressed, EquipMouseInput.Button.Right, Fire));
            inputs.Add(new EquipKeyInput(EquipKeyInput.Types.Pressed, Keys.A, Fire));
        }

        public void Fire()
        {
            Console.WriteLine("Bang!");
        }

        public void Update(GameTime gameTime)
        {
            foreach(EquipInput input in inputs)
            {
                input.Update(gameTime);
            }
        }
    }
}
