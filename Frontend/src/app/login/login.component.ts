import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CookieService } from 'ngx-cookie-service';
import { finalize, first } from 'rxjs/operators';
import { Cliente } from '../models/Cliente';
import { Retorno } from '../models/Retorno';
import { LoginService } from '../services/login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss'],
})
export class LoginComponent implements OnInit {
  focus: any;
  focus1: any;

  loginForm!: FormGroup;
  loading = false;
  submitted = false;
  returnUrl: string | undefined;
  showAlert: boolean = false;
  alertMessage: string | undefined;
  retorno!: Retorno<Cliente>;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private loginService: LoginService,
    private cookieService: CookieService
  ) {}

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      Email: ['', [Validators.required, Validators.email]],
      Senha: ['', [Validators.required]],
    });

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
  }

  // convenience getter for easy access to form fields
  get f() {
    return this.loginForm.controls;
  }

  Entrar() {
    this.submitted = true;
    
    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.loginService
      .Login(this.f.Email.value, this.f.Senha.value)
      .pipe(finalize(() => this.loading = false))
      .subscribe(
        (success) => {          
          this.retorno = success;
          //this.loading = false;
          this.cookieService.set('emb_user', JSON.stringify({Id: `${this.retorno.Dados?.Id}`, Token: this.retorno.Dados?.Token}));
          this.router.navigate([this.returnUrl]);
        },
        (err: HttpErrorResponse) => {
          console.log(err)
          if (err.status == 0) {
            this.alertMessage =
              'Sistema temporariamente indisponível. Tente novamente mais tarde.';
          } else if (err.status > 0) {
            this.retorno = err.error;
            this.alertMessage = this.retorno?.Mensagem;
          }

          this.showAlert = true;
          //this.loading = false;
        }
      );
  }
}
