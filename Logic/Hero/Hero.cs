using Logic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Hero : ITakeDamage, IAttack
    {
        private int _health;
        private int _damage;

        public int GetHealth()
        {
            return _health;
        }
        public int GetDamage()
        {
            return _damage;
        }
        public void SetHealth(int health)
        {
            _health = health;
        }
        public void SetDamage(int damage)
        {
            _damage = damage;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
        }

        public void Attack(ITakeDamage target)
        {
            target.TakeDamage(_damage);
        }
    }
}
