using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GunShoot : MonoBehaviour
{
    public float range, radius, maxDistance;
    public int BulletAmount;
    public Transform muzzle;
    SpawnPickups Spawner;
    Add100Effect ScoreOnDeathDisplay;
    public ParticleSystem explosion;
    public static Vector3 EnemyLocation;
    private Transform explosionPos;
    private Vector3 velocity = Vector3.zero;
    public static Vector3 TargetPos;
    private Vector3 TargetRot;
    public Transform Pistol;
    public ParticleSystem MuzzleFlash;
    public AudioSource ShootSound;
    public Transform Crosshair, Cam;
    public static bool isAiming = false;
    public Transform weaponAimPosition, WeaponDefaultPosition;
    public float AimSpeed, Speed;
    public float defaultFOV, zoomFOV = 40;
    public new Camera camera;
    public float SensitivityXWhenZoomed, SensitivityYWhenZoomed;
    public bool Shooting = false;
    public GameObject CrosshairGO;
    private Vector3 CrosshairStartPos;
    public float Smooth;
    public static int AmountOfKills = 0;
    public static float ValueOfAccuracyWhenAiming = 0.8f;
    public static int DamageDealt = 0;
    public Vector3 offset;
    public static Canvas HealthBarCanvas;
    public Slider Healthbar;
    public static Vector3 EnemyHitLocation;
    public static Transform AICarLocation;
    public RectTransform HealthBarRectTransform;
    public GameObject player;
    public Vector3 NewPos, NewScale;
    private GameObject HitMarker;
    private GameObject PauseMenu;
    [SerializeField] InputActionReference ShootRef, AimRef;
    private float FireBullet, Aim;
    public Toggle InvertToggle;
    public GameObject RebindMenuUI;
    private void Awake()
    {
        PauseMenu = GameObject.Find("PauseMenu");
        Healthbar = GameObject.Find("HealthBarCar").GetComponent<Slider>();
        HealthBarRectTransform = GameObject.Find("HealthBarCanvas").GetComponent<RectTransform>();
        HitMarker = GameObject.Find("HitMarker");
        HealthBarCanvas = GameObject.Find("HealthBarCanvas").GetComponent<Canvas>();
        DamageDealt = 0;
        AmountOfKills = 0;
        ValueOfAccuracyWhenAiming = 0.8f;
        CrosshairGO = GameObject.FindWithTag("Crosshair");
        Crosshair = GameObject.FindWithTag("Crosshair").transform;
        ShootSound = GameObject.FindGameObjectWithTag("GunShotSound").GetComponent<AudioSource>();
        AimRef.action.performed += ctx => Aim = ctx.ReadValue<float>();
        AimRef.action.canceled += ctx => Aim = 0;
        InvertToggle = GameObject.Find("Invert").GetComponent<Toggle>();
        InvertToggle.isOn = false;
        RebindMenuUI = GameObject.Find("ButtonRebindMenu");
        PauseMenu.SetActive(false);
        RebindMenuUI.SetActive(false);
    }
    private void OnEnable()
    {
        AimRef.action.Enable();
        ShootRef.action.Enable();
    }
    private void OnDisable()
    {
        AimRef.action.Disable();
        ShootRef.action.Disable();
    }
    private void Start()
    {
        BulletAmount = 100;
        HitMarker.SetActive(false);
        player = GameObject.Find("PlayerCharacter");
        ParticleSystem explosion = GetComponent<ParticleSystem>();
        ScoreOnDeathDisplay = FindObjectOfType<Add100Effect>();
        Spawner = FindObjectOfType<SpawnPickups>();
        defaultFOV = camera.fieldOfView;
        CrosshairGO.SetActive(true);
        CrosshairStartPos = Crosshair.transform.position;
    }
    
    private void Update()
    {

        Vector3 fwd = muzzle.transform.TransformDirection(Vector3.forward);
        if (isAiming)
        {
            if (!PauseMenu.activeInHierarchy)
            {
                CrosshairGO.SetActive(false);
            }
            if (BulletAmount > 0)
            {
                if (ShootRef.action.triggered)
                {
                    MuzzleFlash.Play();
                    ShootSound.Play();
                    BulletAmount--;
                }
            }
            if(Physics.SphereCast(muzzle.transform.position, radius, fwd, out RaycastHit hit, maxDistance))
            {
                if (hit.collider.gameObject.name == "AICar") //code for setting healthbar to Car
                {
                        if (hit.collider.gameObject.GetComponent<AICar>().AICarHealth > 1)
                        {
                        HealthBarCanvas.transform.SetParent(hit.collider.transform);
                        HealthBarCanvas.transform.LookAt(player.transform);
                        HealthBarRectTransform.localPosition = NewPos;
                        HealthBarRectTransform.localScale = NewScale;
                        Healthbar.value = hit.collider.gameObject.GetComponent<AICar>().AICarHealth;
                        Healthbar.maxValue = hit.collider.gameObject.GetComponent<AICar>().MaxAICarHealth;
                        }
                    }
                    if (hit.collider.gameObject.name == "AISoldier") //code for setting healthbar to soldier
                {
                        if (hit.collider.gameObject.GetComponent<SoldierAI>().EnemyHealth > 1)
                        {
                        HealthBarCanvas.transform.SetParent(hit.collider.transform);
                        HealthBarCanvas.transform.LookAt(player.transform);
                        HealthBarRectTransform.localPosition = new(NewPos.x, 2.8f, NewPos.z);
                        HealthBarRectTransform.localScale = NewScale;
                        Healthbar.value = hit.collider.gameObject.GetComponent<SoldierAI>().EnemyHealth;
                        Healthbar.maxValue = hit.collider.gameObject.GetComponent<SoldierAI>().MaxSoldierHealth;
                        }
                    }
                    if (hit.collider.gameObject.name == "AIZombie") //code for setting healthbar to zombie
                    {
                        if (hit.collider.gameObject.GetComponent<ZombieAi>().ZombieHealth > 1)
                        {
                        HealthBarCanvas.transform.SetParent(hit.collider.transform);
                        HealthBarCanvas.transform.LookAt(player.transform);
                        HealthBarRectTransform.localPosition = new(NewPos.x, 2f, NewPos.y);
                        HealthBarRectTransform.localScale = NewScale;
                        Healthbar.value = hit.collider.gameObject.GetComponent<ZombieAi>().ZombieHealth;
                        Healthbar.maxValue = hit.collider.gameObject.GetComponent<ZombieAi>().MaxZombieHealth;
                        }
                    }
                

                    //Debug.DrawLine(ray.origin, fwd * 15, Color.black);
                    // Debug.DrawRay(camera.transform.position, fwd * 15, Color.black);
                    if (hit.collider.gameObject.name == "AICar" || hit.collider.gameObject.name == "AISoldier" || hit.collider.gameObject.name == "AIZombie")
                    {
                    //HIT = true;
                    //LockedOn = true;
                    // print("LOOK AT TARGET");
                    // print("Aiming and Locked On");
                    //center = transform.position + (transform.forward * hit.distance);
                    TargetPos = hit.collider.gameObject.transform.position;//hit.collider.gameObject.transform.position;//new Vector3(hit.collider.gameObject.transform.position.x, 1f, hit.collider.gameObject.transform.position.z);/*hit.collider.gameObject.transform.position.y, hit.collider.gameObject.transform.position.z*///hit.collider.gameObject.transform.position;
                    // Vector3 dir = TargetPos - camera.transform.position;
                    // camera.transform.rotation = Quaternion.LookRotation(TargetPos);
                    //TargetRot = hit.collider.gameObject.transform.rotation;
                    //Crosshair.transform.rotation = Quaternion.Lerp(Crosshair.transform.rotation, Quaternion.LookRotation(Target, center), Time.deltaTime * Smooth);
                    //Pistol.transform.LookAt(Target);
                    // transform.LookAt(TargetPos);
                    //Pistol.LookAt(TargetPos);
                    var rotate = Quaternion.LookRotation(TargetPos - transform.position);
                    transform.rotation = Quaternion.Lerp(transform.rotation, rotate, Smooth * Time.deltaTime);
                    //Pistol.rotation = Quaternion.LookRotation(TargetPos - Pistol.position);
                    //transform.LookAt(TargetPos);
                    if (BulletAmount > 0)
                    {
                        if (ShootRef.action.triggered)
                        {
                            if (hit.collider.gameObject.name == "AICar")
                            {
                                float AimingAccuracy = Random.value;
                                print(AimingAccuracy);
                                if (AimingAccuracy < ValueOfAccuracyWhenAiming)
                                {
                                    HitMarker.SetActive(true);
                                    StartCoroutine(nameof(EnableHitMarker));
                                    EnemyLocation = hit.collider.gameObject.GetComponent<AICar>().transform.position;
                                    hit.collider.gameObject.GetComponent<AICar>().AICarHealth -= 100;
                                    DamageDealt += 100;
                                    if (hit.collider.gameObject.GetComponent<AICar>().AICarHealth < 1)
                                    {
                                        HitMarker.GetComponent<Image>().color = new Color(255f, 0f, 0f);
                                        StartCoroutine(nameof(ResetColour));
                                        HealthBarCanvas.transform.SetParent(null);
                                        Destroy(hit.collider.gameObject);
                                        AmountOfKills += 1;
                                        ScoreUI.score += 100;
                                        PlayExplosion();
                                        Spawner.SpawnPickupOnDeath();
                                        ScoreOnDeathDisplay.ScoreAdditionUI.enabled = true;
                                        ScoreOnDeathDisplay.Add100();
                                    }
                                }
                            }
                            if (hit.collider.gameObject.name == "AISoldier")
                            {
                                //print(AICar.AICarHealth);
                                float AimingAccuracy = Random.value;
                                print(AimingAccuracy);
                                if (AimingAccuracy < ValueOfAccuracyWhenAiming)
                                {
                                                                     
                                    HitMarker.SetActive(true);
                                    StartCoroutine(nameof(EnableHitMarker));
                                    HitMarker.GetComponent<Image>().color = new Color(255f, 0f, 0f);
                                    StartCoroutine(nameof(ResetColour));
                                    EnemyLocation = hit.collider.gameObject.GetComponent<SoldierAI>().transform.position;
                                    hit.collider.gameObject.GetComponent<SoldierAI>().EnemyHealth -= 100;
                                    DamageDealt += 100;
                                    if (hit.collider.gameObject.GetComponent<SoldierAI>().EnemyHealth < 1)
                                    {
                                        HitMarker.GetComponent<Image>().color = new Color(255f, 0f, 0f);
                                        StartCoroutine(nameof(ResetColour));
                                        HealthBarCanvas.transform.SetParent(null);
                                        Destroy(hit.collider.gameObject);
                                        AmountOfKills += 1;
                                        ScoreUI.score += 50;
                                        PlayExplosion();
                                        Spawner.SpawnPickupOnDeath();
                                        ScoreOnDeathDisplay.ScoreAdditionUI.enabled = true;
                                        ScoreOnDeathDisplay.Add50();
                                    }
                                }
                            }
                            if (hit.collider.gameObject.name == "AIZombie")
                            {
                                //print(AICar.AICarHealth);
                                float AimingAccuracy = Random.value;
                                print(AimingAccuracy);
                                if (AimingAccuracy < ValueOfAccuracyWhenAiming)
                                {
                                    HitMarker.SetActive(true);
                                    StartCoroutine(nameof(EnableHitMarker));
                                    EnemyLocation = hit.collider.gameObject.GetComponent<ZombieAi>().transform.position;
                                    hit.collider.gameObject.GetComponent<ZombieAi>().ZombieHealth -= 100;
                                    DamageDealt += 100;
                                    if (hit.collider.gameObject.GetComponent<ZombieAi>().ZombieHealth < 1)
                                    {
                                        HitMarker.GetComponent<Image>().color = new Color(255f, 0f, 0f);
                                        StartCoroutine(nameof(ResetColour));
                                        HealthBarCanvas.transform.SetParent(null);
                                        Destroy(hit.collider.gameObject);
                                        AmountOfKills += 1;
                                        ScoreUI.score += 300;
                                        PlayExplosion();
                                        Spawner.SpawnPickupOnDeath();
                                        ScoreOnDeathDisplay.ScoreAdditionUI.enabled = true;
                                        ScoreOnDeathDisplay.Add300();
                                    }
                                }
                            }
                        }
                    }
                    // camera.transform.eulerAngles = camera.transform.eulerAngles - TargetPos;
                    //camera.transform.LookAt(Crosshair.position);
                    //Cam.LookAt(TargetPos);
                    //camera.transform.LookAt(hit.collider.transform.position);
                    //camera.transform.LookAt(hit.transform);
                    //camera.transform.LookAt(hit.point);
                    //camera.transform.LookAt(Crosshair.InverseTransformPoint(TargetPos));
                    //camera.transform.eulerAngles = camera.WorldToScreenPoint(TargetPos);
                    //camera.transform.localRotation = Quaternion.Euler(TargetRot.x, TargetRot.y, TargetRot.z);
                    // Crosshair.transform.LookAt(Target);
                    //camera.transform.rotation = TargetRot;
                    //camera.transform.rotation = new Vector3(Target);
                    //camera.transform.LookAt(TargetPos);
                    //Pistol.transform.LookAt(Crosshair.transform.position);
                }
                else
                {
                    //Crosshair.transform.position = CrosshairStartPos;                    
                }
            }
            /*else if (HitAICar == true)
            {
                // not anymore.
                HitAICar = false;
            }*/
        }
        else if (!isAiming)
        {
            if (!PauseMenu.activeInHierarchy)
            {
                CrosshairGO.SetActive(true);
            }
            if (BulletAmount > 0) // Not aiming so no raycast, (Hipfire)
            {//Lower accuracy unless witin a sphere collider around the player
                if (ShootRef.action.triggered)
                {
                    MuzzleFlash.Play();
                    ShootSound.Play();
                    BulletAmount--;
                    if (Physics.Raycast(muzzle.transform.position, muzzle.forward, out RaycastHit hit, range))
                    {
                        if (hit.collider.gameObject.name == "AICar")
                        {
                            float HipFireAccuracy = Random.value;
                            print(HipFireAccuracy);
                            if (HipFireAccuracy < 0.3)
                            {
                                StartCoroutine(nameof(DisableCrosshair));
                                EnemyLocation = hit.collider.gameObject.GetComponent<AICar>().transform.position;
                                hit.collider.gameObject.GetComponent<AICar>().AICarHealth -= 100;
                                DamageDealt += 100;
                                if (hit.collider.gameObject.GetComponent<AICar>().AICarHealth < 1)
                                {
                                    Destroy(hit.collider.gameObject);
                                    AmountOfKills += 1;
                                    ScoreUI.score += 100;
                                    PlayExplosion();
                                    Spawner.SpawnPickupOnDeath();
                                    ScoreOnDeathDisplay.ScoreAdditionUI.enabled = true;
                                    ScoreOnDeathDisplay.Add100();
                                }
                            }
                        }
                        if (hit.collider.gameObject.name == "AISoldier")
                        {
                            float HipFireAccuracy = Random.value;
                            print(HipFireAccuracy);
                            if (HipFireAccuracy < 0.3)
                            {
                                //print(AICar.AICarHealth);
                                EnemyLocation = hit.collider.gameObject.GetComponent<SoldierAI>().transform.position;
                                hit.collider.gameObject.GetComponent<SoldierAI>().EnemyHealth -= 100;
                                DamageDealt += 100;
                                if (hit.collider.gameObject.GetComponent<SoldierAI>().EnemyHealth < 1)
                                {
                                    Destroy(hit.collider.gameObject);
                                    AmountOfKills += 1;
                                    ScoreUI.score += 50;
                                    PlayExplosion();
                                    Spawner.SpawnPickupOnDeath();
                                    ScoreOnDeathDisplay.ScoreAdditionUI.enabled = true;
                                    ScoreOnDeathDisplay.Add50();
                                }
                            }
                        }

                        if (hit.collider.gameObject.name == "AIZombie")
                        {
                            float HipFireAccuracy = Random.value;
                            print(HipFireAccuracy);
                            if (HipFireAccuracy < 0.3)
                            {
                                //print(AICar.AICarHealth);
                                EnemyLocation = hit.collider.gameObject.GetComponent<ZombieAi>().transform.position;
                                hit.collider.gameObject.GetComponent<ZombieAi>().ZombieHealth -= 100;
                                DamageDealt += 100;
                                if (hit.collider.gameObject.GetComponent<ZombieAi>().ZombieHealth < 1)
                                {
                                    Destroy(hit.collider.gameObject);
                                    AmountOfKills += 1;
                                    ScoreUI.score += 300;
                                    PlayExplosion();
                                    Spawner.SpawnPickupOnDeath();
                                    ScoreOnDeathDisplay.ScoreAdditionUI.enabled = true;
                                    ScoreOnDeathDisplay.Add300();
                                }
                            }
                        }
                    }
                }
            }
        }
    
       /* if(Physics.Raycast(camera.transform.position, camera.transform.forward, maxDistance))
        {
            Debug.DrawRay(camera.transform.position, camera.transform.forward);
        }*/
        //MuzzleFlash.transform.parent = Pistol.transform;
        if (Aim > 0)
        {
            print("SHOOT");
            isAiming = true;
        }
        else
        {
            print("stop shoot");
            isAiming = false;
        }
        if (PauseMenu.activeInHierarchy)
        {
            CrosshairGO.SetActive(false);
        }
        //Debug.DrawRay(muzzle.transform.position, muzzle.forward);
        
    }
    private void FixedUpdate()
    {
        if (Time.timeScale == 1)
        {
            if (isAiming)
            {
                //print("AIM");
                Pistol.position = Vector3.SmoothDamp(Pistol.position, weaponAimPosition.position, ref velocity, AimSpeed * Time.deltaTime);
                //transform.position = Vector3.Lerp(transform.position, weaponAimPosition.transform.position, AimSpeed * Time.deltaTime);            
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, zoomFOV, AimSpeed * Time.deltaTime);
                CameraRotation.sensX = SensitivityXWhenZoomed;
                CameraRotation.sensY = SensitivityYWhenZoomed;
                if (!InvertToggle.isOn)
                {
                    CameraRotation.sensX = SensitivityXWhenZoomed;
                    CameraRotation.sensY = SensitivityYWhenZoomed;
                }
                else
                {
                    CameraRotation.sensX = SensitivityXWhenZoomed * -1;
                    CameraRotation.sensY = SensitivityYWhenZoomed * -1;
                }
                //SetFOV(Mathf.Lerp(playerCam.fieldOfView, zoomRatio, AimSpeed * Time.deltaTime));
            }
            if (!isAiming)
            {
                //print("No Aim");
                if(Pistol.position != WeaponDefaultPosition.position)
                {
                    print("Do tHis");
                }
                Pistol.position = Vector3.SmoothDamp(Pistol.position, WeaponDefaultPosition.position, ref velocity, AimSpeed * Time.deltaTime);
                camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, defaultFOV, AimSpeed * Time.deltaTime);
                print(CameraRotation.sensX);
                if (!InvertToggle.isOn)
                {
                    CameraRotation.sensX = PlayerPrefs.GetFloat("SensX");
                    CameraRotation.sensY = PlayerPrefs.GetFloat("SensY");
                }
                else
                {
                    CameraRotation.sensX = PlayerPrefs.GetFloat("SensX") * -1;
                    CameraRotation.sensY = PlayerPrefs.GetFloat("SensY") * -1;
                }
                //camera.fieldOfView = defaultFOV;
            }
        }
    }

    void PlayExplosion()
    {
        Instantiate(explosion, EnemyLocation, explosion.transform.rotation);
    }
    IEnumerator EnableHitMarker()
    {
        yield return new WaitForSeconds(0.15f);
        HitMarker.SetActive(false);
    }
    IEnumerator DisableCrosshair()
    {
        yield return new WaitForSeconds(0.05f);
        CrosshairGO.SetActive(true);
    }
    IEnumerator ResetColour()
    {
        yield return new WaitForSeconds(0.15f);
        HitMarker.GetComponent<Image>().color = new Color(255f,255f,255f);
    }
   private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Vector3 fwd = camera.transform.TransformDirection(Vector3.forward) * 10;
        Gizmos.DrawWireSphere(camera.transform.position + fwd * 2, radius);
    }
}
