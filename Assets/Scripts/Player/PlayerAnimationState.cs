// Los estados sin arma primero y con arma despues, en el mismo orden. Lo mismo con el estado de disparo en el suelo y en el aire
// PlayerAnimator calcula la variante armada sumando ArmedOffset (= 3), asi que el orden debe ser estricto.
// Si se agrega un estado con variante armada y desarmada en un futuro, se debe seguir el orden y "ArmedOffset" debe ser actualizado segun la cantidad de estados con variante (= 4 con un estado mas).
public enum PlayerAnimationState
{
    Idle = 0,
    Run = 1,
    Jump = 2,

    IdleArmed = 3,
    RunArmed = 4,
    JumpArmed = 5,

    // Disparar implica tener el arma, su variante es piso / aire
    Shoot = 6,
    ShootRun = 7,
    ShootAir = 8,

    Death = 9
}