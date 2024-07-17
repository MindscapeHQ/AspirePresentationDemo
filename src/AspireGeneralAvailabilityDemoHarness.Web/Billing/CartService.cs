namespace AspireGeneralAvailabilityDemoHarness.Web.Billing
{
  public class CartService
  {
    public async Task AddToCart(int raygunId)
    {
      var httpClient = new HttpClient();

      var response = await httpClient.PostAsJsonAsync("api/cart", new { RaygunId = raygunId });

      response.EnsureSuccessStatusCode();
    }
  }
}
