using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    /*
     * �Ѿ� -> LineRender -> RatCast
     * �ѻ�Ÿ� 
     * �߻�� ��ġ 
     * Gundata��������
     * Effect
     * ���� ���� -> Enum
     *              ������
     *              źâ�� ����� ��
     *              �߻� �غ�
     * audio Source
     * 
     * Method
     *  �߻� -> Fire
     *  Reload
     *  Effect Play
     */

    public enum State
    {
        Ready, //�߻��غ�
        Empty, //�Ѿ� ��
        Reloading,// ������
    }
    public State state { get; private set; }
    //�Ѿ��� �߻�� ��ġ
    public Transform Fire_Transform;
    //c�Ѿ� Line Renderer
    public LineRenderer lineRenderer;
    //�Ѿ� �߻� �Ҹ�
    private AudioSource audioSource;
    //�� ��Ÿ�
    private float Distance = 50f;
    //�� Data
    public GunData data;

    public ParticleSystem shot_Effect;
    public ParticleSystem shell_Effect;

    private float LastFireTime;

    public int ammoRemain = 100;
    public int Magammo;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        lineRenderer= GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;

        //������Ʈ ��Ȱ��ȭ
        lineRenderer.enabled = false;
    }

    private void OnEnable()
    {
        ammoRemain = data.StartAmmoRemaion;//�Ѿ� �ʱ�ȭ
        Magammo = data.MagCapacity;//���� �Ѿ� �ʱ�ȭ

        state = State.Ready;//�� ����Ȯ��

        LastFireTime = 0;

    }
    //�� �߻� �޼���
    public void Fire()
    {
        //�÷��̾��� ���� �� ���°� �غ�����̸鼭
        //������ �߻�ð��� ���� �ð����� ���� �� �߻� ����
        if(state.Equals(State.Ready)&&Time.time>=LastFireTime+data.TimebetFire)
        {
            LastFireTime = Time.time;
            //�߻� 
            Shot();
        }
    }

    public void Shot()
    {
        //�� -> Raycast
        RaycastHit hit;
        Vector3 HitPosition=Vector3.zero;

        if (Physics.Raycast(Fire_Transform.position, Fire_Transform.forward, out hit, Distance))
        {
            //�Ѿ��� �¾��� ���
            //�츮�� ���� �������̽��� ������ �ͼ�
            //���� ������Ʈ ���� ������ �����
            IDamage target = hit.collider.GetComponent<IDamage>();

            if (target!=null) 
            {
                target.OnDamage(data.Damage, hit.point, hit.normal);
            }
            HitPosition = hit.point;
        }
        else
        {
            //Ray�� �ٸ� ��ü�� �浹���� �ʾ��� ���
            //ź���� �ִ� �����Ÿ����� ���� ���� ��
            HitPosition = Fire_Transform.position + Fire_Transform.forward * Distance;
        }
        //���� �� ����Ʈ �÷���
        StartCoroutine(ShotEffect(HitPosition));
        Magammo--;
        if(Magammo<=0)
        {
            state = State.Empty;//�ѻ��º���
        }
    }
    private IEnumerator ShotEffect(Vector3 Hitposition)
    {
        shot_Effect.Play();
        shell_Effect.Play();
        //�Ҹ�
        audioSource.PlayOneShot(data.Shot_clip);

        //���η����� ����
        lineRenderer.SetPosition(0,Fire_Transform.position);//������
        lineRenderer.SetPosition(1,Hitposition);//����
        //���� �׸� �⸰ �׸�
        lineRenderer.enabled = true;
        yield return new WaitForSeconds(0.03f);//�����ֱ�, �ð�����ϴ°�
        lineRenderer.enabled = false;


    }

    public bool Reload()
    {
        //���� �������� �ʿ����� ������ Retrun���� �޼ҵ�
        //�̹� ������ �� �̰ų�, �Ѿ��� ���ų�, źâ�� �̹� �Ѿ��� ������ ���(30���ΰ��)
        //false�� �����Ұ��̴�.
        
        if(state.Equals(State.Reloading)||ammoRemain<=0||Magammo>=data.MagCapacity)
        {
            return false;
        }
        //���� �� �� �ִ� ����
        StartCoroutine(Reload_co());
        return true;
    }
    private IEnumerator Reload_co()
    {
        state = State.Reloading;
        audioSource.PlayOneShot(data.Reload_clip);
        yield return new WaitForSeconds(data.ReloadTime);

        //������ �� ���
        int ammofill = data.MagCapacity - Magammo;
        //źâ�� ä���� �� ź���� ���� ź�ຸ�� ���ٸ�
        //ä������ ź����� ���� ź�� ���� ���� ���δ�.
        if(ammoRemain<ammofill)
        {
            ammofill = ammoRemain;
        }
        //źâ�� ä��� ��ü źâ�� ���� ���δ�.
        Magammo += ammofill;
        ammoRemain -= ammofill;
        state = State.Ready;
    }



}