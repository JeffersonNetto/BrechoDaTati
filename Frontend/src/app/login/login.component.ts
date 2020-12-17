import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { first } from 'rxjs/operators';
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
  showErrorAlert: boolean = false
  errorMessage: string | undefined

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private loginService: LoginService
  ) {}

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      Email: ['', [Validators.required, Validators.email]],
      Senha: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(20),
        ],
      ],
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

    // stop here if form is invalid
    if (this.loginForm.invalid) {
      return;
    }

    this.loading = true;
    this.loginService
      .Login(this.f.Email.value, this.f.Senha.value)
      .pipe(first())
      .subscribe(
        (data) => {
          console.log(data);
          this.router.navigate([this.returnUrl]);
        },
        (err: HttpErrorResponse) => {          
          console.warn(err)
          this.showErrorAlert = true;
          this.errorMessage = err.error?.mensagem

          if(!this.errorMessage) {
            this.errorMessage = 'Sistema temporariamente indisponível. Tente novamente mais tarde.'
          }

          this.loading = false;
        }
      );
  }
}
