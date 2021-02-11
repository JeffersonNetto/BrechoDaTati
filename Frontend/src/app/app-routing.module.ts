import { ProductsComponent } from './products/products.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { ProductComponent } from './product/product.component';
import { AuthGuardService } from './services/auth-guard.service';
import { ProfileComponent } from './profile/profile.component';
import { CartComponent } from './cart/cart.component';
import { LogoutComponent } from './logout/logout.component';

const routes: Routes = [
  { path: 'home', component: HomeComponent },
  { path: 'login', component: LoginComponent },
  { path: 'logout', component: LogoutComponent },
  { path: 'register', component: SignupComponent },
  { path: 'produtos', component: ProductsComponent },
  { path: 'produto/:slug', component: ProductComponent },
  {
    path: 'perfil',
    component: ProfileComponent,
    canActivate: [AuthGuardService],
  },
  {
    path: 'carrinho',
    component: CartComponent,
    //canActivate: [AuthGuardService],
  },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
