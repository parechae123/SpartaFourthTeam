public interface IDamageable
{
    public int Health { get; set; }
    public void OnDamageAppllied(int damage);
}
