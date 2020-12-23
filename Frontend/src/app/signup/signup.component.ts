import { HttpErrorResponse } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { finalize, first } from 'rxjs/operators';
import { Helpers } from '../helpers/helpers';
import { Cliente } from '../models/Cliente';
import { Retorno } from '../models/Retorno';
import { RegisterService } from '../services/register.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.scss'],
})
export class SignupComponent implements OnInit {
  test: Date = new Date();
  focus: any;
  focus1: any;
  focus2: any;
  registerForm!: FormGroup;
  loading: boolean = false;
  submitted = false;
  cliente!: Cliente;
  termos: boolean = false;
  showAlert: boolean = false;
  alertMessage: string | undefined;
  forcaDaSenha: string | undefined;
  success: boolean | undefined;
  retorno!: Retorno<Cliente>

  constructor(
    private formBuilder: FormBuilder,
    private registerService: RegisterService
  ) {}

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      Nome: [
        '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(100),
        ],
      ],
      Sobrenome: [
        '',
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(100),
        ],
      ],
      Cpf: [
        '',
        [
          Validators.required,
          Validators.minLength(11),
          Validators.maxLength(11),
        ],
      ],
      DataNascimento: ['', Validators.required],
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
  }

  get f() {
    return this.registerForm.controls;
  }

  AceitarTermos() {
    this.termos = !this.termos;
  }

  Cadastrar() {
    this.submitted = true;
    
    if (this.registerForm.invalid) {
      return;
    }

    this.loading = true;

    this.cliente = Object.assign({}, this.cliente, this.registerForm.value);
    
    this.registerService
      .Register(this.cliente)
      .pipe(finalize(() => this.loading = false))
      .subscribe(
        (success) => {
          this.retorno = success;                    
          this.alertMessage = this.retorno.Mensagem
          this.showAlert = true;
          this.success = true;
          this.loading = false;
        },
        (err: HttpErrorResponse) => {

          if (err.status == 0) {
            this.alertMessage =
              'Sistema temporariamente indisponível. Tente novamente mais tarde.';
          } else if (err.status > 0) {
            this.retorno = err.error;
            this.alertMessage = this.retorno.Mensagem;
          }

          this.showAlert = true;
          this.success = false;          
        },        
      );
  }

  VerificarForcaDaSenha() {
    this.forcaDaSenha = Helpers.VerificarForcaDaSenha(this.f.Senha.value);       
  }
}
