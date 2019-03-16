#pragma checksum "/home/bibiana/FASEM/PruebaI/DemoBlazor/Pages/RegistrationForm.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2112b7393c43f7f8d99f596be393683c83b5f156"
// <auto-generated/>
#pragma warning disable 1591
#pragma warning disable 0414
#pragma warning disable 0649
#pragma warning disable 0169

namespace DemoBlazor.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Blazor;
    using Microsoft.AspNetCore.Blazor.Components;
    using System.Net.Http;
    using Microsoft.AspNetCore.Blazor.Layouts;
    using Microsoft.AspNetCore.Blazor.Routing;
    using Microsoft.JSInterop;
    using DemoBlazor;
    using DemoBlazor.Shared;
    using SharedLibrary;
    [Microsoft.AspNetCore.Blazor.Layouts.LayoutAttribute(typeof(MainLayout))]

    [Microsoft.AspNetCore.Blazor.Components.RouteAttribute("/registrationform")]
    public class RegistrationForm : Microsoft.AspNetCore.Blazor.Components.BlazorComponent
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Blazor.RenderTree.RenderTreeBuilder builder)
        {
        }
        #pragma warning restore 1998
#line 31 "/home/bibiana/FASEM/PruebaI/DemoBlazor/Pages/RegistrationForm.cshtml"
            
    bool registrationComplete = false;
    RegistrationData model { get; set; }


    protected override void OnInit()
    {
        base.OnInit();
        model = new RegistrationData() { FirstName = "test", LastName = "test", Email = "test@test.com", Phone = "1234567890" };
        model.ModelChanged += ModelChanged;
        model.CheckRules();
    }

    private void ModelChanged(object sender, EventArgs e)
    {
        base.StateHasChanged();
    }

    async Task Register()
    {
        await Http.PostJsonAsync<RegistrationData>("https://localhost:44332/api/Registration", model);
        registrationComplete = true;
    }

#line default
#line hidden
        [global::Microsoft.AspNetCore.Blazor.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591