builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");
app.UseRequestLocalization(new RequestLocalizationOptions {
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("es-ES") },
    SupportedUICultures = new List<CultureInfo> { new CultureInfo("en-US"), new CultureInfo("es-ES") }
});
