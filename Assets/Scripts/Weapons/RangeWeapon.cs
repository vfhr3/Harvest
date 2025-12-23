namespace Weapons
{
    public abstract class RangeWeapon : BaseWeapon
    {
        private RangeWeaponConfig _config;
        protected RangeWeapon(RangeWeaponConfig config) : base(config)
        {
            _config = config;
        }
    }

    public class RangeWeaponConfig : WeaponConfig
    {
        public float Lifespan;
        public float ProjectileSpeed;
        public int ProjectileCount;
        public int PierceCount;
        public int ForkCount;
    }
}