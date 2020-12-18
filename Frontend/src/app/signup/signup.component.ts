import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
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

    // stop here if form is invalid
    // if (this.registerForm.invalid) {
    //   return;
    // }

    this.loading = true;

    this.cliente = Object.assign({}, this.cliente, this.registerForm.value);
    
    this.registerService
      .Register(this.cliente)
      //.pipe(first())
      .subscribe(
        (success) => {
          this.retorno = success;                    
          this.alertMessage = this.retorno.Mensagem
          this.showAlert = true;
          this.success = true;
          this.loading = false;
        },
        (err) => {
          this.retorno = err.error
          this.alertMessage = this.retorno.Mensagem
          this.showAlert = true;
          this.success = false;
          this.loading = false;
        },        
      );
  }

  VerificarForcaDaSenha() {
    let password = this.f.Senha.value;
    var caracteresEspeciais = '!£$%^&*_@#~?';
    var passwordPontos = 0;

    // Contém caracteres especiais
    for (var i = 0; i < password.length; i++) {
      if (caracteresEspeciais.indexOf(password.charAt(i)) > -1) {
        passwordPontos += 20;
        break;
      }
    }
    // Contém numeros
    if (/\d/.test(password)) passwordPontos += 20;
    // Contém letras minúsculas
    if (/[a-z]/.test(password)) passwordPontos += 20;
    // Contém letras maiúsculas
    if (/[A-Z]/.test(password)) passwordPontos += 20;
    if (password.length >= 8) passwordPontos += 20;
    var forcaSenha = '';
    var backgroundColor = 'red';
    if (passwordPontos >= 100) {
      forcaSenha = 'Forte';
      backgroundColor = 'green';
    } else if (passwordPontos >= 80) {
      forcaSenha = 'Média';
      backgroundColor = 'gray';
    } else if (passwordPontos >= 60) {
      forcaSenha = 'Fraca';
      backgroundColor = 'maroon';
    } else {
      forcaSenha = 'Muito Fraca';
      backgroundColor = 'red';
    }
    this.forcaDaSenha = forcaSenha;
  }
}
