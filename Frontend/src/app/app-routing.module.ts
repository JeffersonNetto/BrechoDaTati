import { ProductsComponent } from './products/products.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { SignupComponent } from './signup/signup.component';
import { ProductComponent } from './product/product.component';

const routes: Routes = [
  { path: 'home',             component: HomeComponent },
  { path: 'login',          component: LoginComponent },
  { path: 'register',           component: SignupComponent },
  { path: 'produtos',           component: ProductsComponent },
  { path: 'produto',           component: ProductComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
