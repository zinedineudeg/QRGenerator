using QRCoder;
string policy = "MyPolicy";
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name:policy, build =>
    {
        build.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost").AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseCors(policy);

app.MapGet("/", () => "Bienvenido");
app.MapGet("/qr", (string text) =>
{
    var qrGenerator= new QRCodeGenerator();
    var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
    BitmapByteQRCode bitmapByteQRCode = new BitmapByteQRCode(qrCodeData);
    var bitMap = bitmapByteQRCode.GetGraphic(20);
    using var ms = new MemoryStream();
    ms.Write(bitMap);
    byte[] bytImage = ms.ToArray();
    return Convert.ToBase64String(bytImage);


});
app.Run();
