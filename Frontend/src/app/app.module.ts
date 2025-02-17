import { BrowserModule } from '@angular/platform-browser';
import { LOCALE_ID, NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { FooterComponent } from './shared/footer/footer.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { ProductsComponent } from './products/products.component';
import { ProductComponent } from './product/product.component';
import { CartComponent } from './cart/cart.component';
import { ProductCardComponent } from './product-card/product-card.component';
import { ProductService } from './services/product.service';
import { HttpClientModule } from '@angular/common/http';
import { LoginService } from './services/login.service';
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';
import { CookieService } from 'ngx-cookie-service';
import { ProfileComponent } from './profile/profile.component';
import { InterceptorModule } from './helpers/interceptor.module';
import { NumbersOnlyDirective } from './directives/numbers-only.directive';
import { AddressFormComponent } from './address-form/address-form.component';
import { PasswordFormComponent } from './password-form/password-form.component'
import { BrowserAnimationsModule, NoopAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { LogoutComponent } from './logout/logout.component';
import { CheckoutComponent } from './checkout/checkout.component';

registerLocaleData(localePt);

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    FooterComponent,
    HomeComponent,
    LoginComponent,
    SignupComponent,
    ProductsComponent,
    ProductComponent,
    CartComponent,
    ProductCardComponent,
    ProfileComponent,
    NumbersOnlyDirective,
    AddressFormComponent,
    PasswordFormComponent,
    LogoutComponent,
    CheckoutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    NgbModule,
    RouterModule,
    HttpClientModule,
    ReactiveFormsModule,
    InterceptorModule,
    NoopAnimationsModule,
    ToastrModule.forRoot()
  ],
  providers: [
    ProductService,
    LoginService,
    { provide: LOCALE_ID, useValue: 'pt-BR' },
    CookieService
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
