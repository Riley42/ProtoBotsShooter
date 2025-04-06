using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO startingWeapon;
    [SerializeField] CinemachineVirtualCamera playerFollowCamera;
    [SerializeField] Camera weaponCamera;
    [SerializeField] GameObject zoomVignette;
    [SerializeField] TextMeshProUGUI ammoText;

    int currentAmmo;
    float defaultFOV;
    float defaultRotationSpeed;
    float timeSinceLastShot = 0;
    Animator anim;
    Weapon currentWeapon;
    WeaponSO currentWeaponSO;
    FirstPersonController fpc;
    StarterAssetsInputs starterAssetInputs;

    readonly int SHOOT = Animator.StringToHash("Shoot");

    private void Awake()
    {
        starterAssetInputs = GetComponentInParent<StarterAssetsInputs>();
        fpc = GetComponentInParent<FirstPersonController>();
        anim = GetComponentInParent<Animator>();
        currentWeapon = GetComponentInChildren<Weapon>();
        defaultFOV = playerFollowCamera.m_Lens.FieldOfView;
        defaultRotationSpeed = fpc.RotationSpeed;

    }
    private void Start()
    {
        SwitchWeapon(startingWeapon);
        AdjustAmmo(currentWeaponSO.MagazineSize);
    }
    private void Update()
    {
        HandleShoot();
        HandleZoom();
    }

    public void AdjustAmmo(int amt)
    {
        currentAmmo += amt;

        currentAmmo = Mathf.Min(currentAmmo, currentWeaponSO.MagazineSize);
        ammoText.text = currentAmmo.ToString("D2");
    }
    private void HandleShoot()
    {
        timeSinceLastShot += Time.deltaTime;

        if (!starterAssetInputs.shoot) return;

        if (timeSinceLastShot >= currentWeaponSO.FireRate && currentAmmo > 0)
        {
            currentWeapon.Shoot(currentWeaponSO);
            anim.Play(SHOOT, 0, 0f);
            timeSinceLastShot = 0f;
            AdjustAmmo(-1);
        }

        if (!currentWeaponSO.IsAutomatic)
        {
            starterAssetInputs.ShootInput(false);
        }        
    }
    void HandleZoom()
    {
        if (!currentWeaponSO.CanZoom) return;

        if (starterAssetInputs.zoom)
        {
            weaponCamera.fieldOfView = currentWeaponSO.ZoomAmount;
            zoomVignette.SetActive(true);
            fpc.ChangeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
        }
        else
        {
            weaponCamera.fieldOfView = defaultFOV;
            fpc.ChangeRotationSpeed(defaultRotationSpeed);

            zoomVignette.SetActive(false);
        }
    }
    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon) 
            Destroy(currentWeapon.gameObject);

        Weapon newWeapon = Instantiate(weaponSO.WeaponPrefab, transform).GetComponent<Weapon>();
        currentWeapon = newWeapon;
        this.currentWeaponSO = weaponSO;

        AdjustAmmo(currentWeaponSO.MagazineSize);
    }
}
