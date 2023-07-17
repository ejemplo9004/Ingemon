using System.Collections;
using UnityEngine;

public class IngemonShop : ItemShop
{
    public IEnumerator BuyIngemonCoroutine(int playerMoney, Ingemonster newIngemon)
    {
        imLoading.SetActive(true);
        UpdatePlayerMoney(playerMoney);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !server.ocupado);
        yield return new WaitUntil(() => moneySubstracter.Done);
        AddIngemonToPlayer(newIngemon);
        yield return new WaitForSeconds(0.5f);
        yield return new WaitUntil(() => !server.ocupado);
        moneySubstracter.Done = false;
        imLoading.SetActive(false);
    }

    private void AddIngemonToPlayer(Ingemonster newIngemon)
    {
        var data = new string[4];
        data[0] = newIngemon.name;
        data[1] = newIngemon.phenotype;
        data[2] = newIngemon.maxHealth.ToString();
        data[3] = GameController.gameController.usuarioActual.id.ToString();
        StartCoroutine(server.ConsumirServicio("guardar ingemon", data, GetServiceResponse));
    }
}