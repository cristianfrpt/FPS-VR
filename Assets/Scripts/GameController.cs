using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject inimigoObj;
    private GameObject[] inimigosSpawnados;
    private int quantInimigos;
    private Vector3[] spawnPoints;
    private bool podeSpawnar = true;

    // Start is called before the first frame update
    void Start()
    {
        quantInimigos = 0;
        inimigosSpawnados = new GameObject[15];
        spawnPoints = new Vector3[8];
        spawnPoints[0] = new Vector3(31f, 0.1f, 3f);
        spawnPoints[1] = new Vector3(27f, 0.1f, 22f);
        spawnPoints[2] = new Vector3(27f, 0.1f, 22f);
        spawnPoints[3] = new Vector3(22f, 0.1f, 18f);
        spawnPoints[4] = new Vector3(36f, 0.1f, 16f);
        spawnPoints[5] = new Vector3(39f, 0.1f, 26f);
        spawnPoints[6] = new Vector3(15f, 0.1f, 15f);
    }

    // Update is called once per frame
    void Update()
    {
        if (quantInimigos < 10 && podeSpawnar) { 
            spawnInimigo();
            podeSpawnar = false;
            StartCoroutine(tempoRespawn());
        }
        deletaInimigosMortos();
    }

    void spawnInimigo()
    {
        for(int i = 0; i < inimigosSpawnados.Length; i++)
        {
            if (inimigosSpawnados[i] == null)
            {
                Vector3 spawnPoint = randomizaSpawn();
                GameObject novoInimigo = Instantiate(inimigoObj);
                novoInimigo.SetActive(true);
                novoInimigo.transform.position = spawnPoint;

                inimigosSpawnados[i] = novoInimigo;
                quantInimigos++;
                break;
            }
        }
    }

    Vector3 randomizaSpawn()
    {
        int randomChoice = Random.Range(0, 7);
        return spawnPoints[randomChoice];
    }

    void deletaInimigosMortos()
    {
        for (int i = 0; i < inimigosSpawnados.Length; i++)
        {
            if(inimigosSpawnados[i] != null) { 
                if (inimigosSpawnados[i].GetComponent<InimigoCode>().enabled == false)
                {
                    StartCoroutine(DesabilitarApos3Segundos(inimigosSpawnados[i]));
                }
            }
        }
    }


    IEnumerator DesabilitarApos3Segundos(GameObject inimigo)
    {
        yield return new WaitForSeconds(3f);
        inimigo.SetActive(false);
        Destroy(inimigo);
        if (quantInimigos > 0)
            quantInimigos--;
    }

    IEnumerator tempoRespawn()
    {
        yield return new WaitForSeconds(4f);
        podeSpawnar = true;
    }
}
