import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CookieService } from 'ngx-cookie-service';
import { finalize, map, tap } from 'rxjs/operators';
import { Cliente } from '../models/Cliente';
import { Retorno } from '../models/Retorno';
import { CacheService } from '../services/cache.service';
import { ProfileService } from '../services/profile.service';
import * as moment from 'moment';
import { RegisterService } from '../services/register.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ClienteEndereco } from '../models/ClienteEndereco';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'],
})
export class ProfileComponent implements OnInit {
  test: Date = new Date();
  focus: any;
  focus1: any;
  focus2: any;
  cliente!: Cliente;
  page = 2;
  page1 = 3;
  registerForm: FormGroup = new FormGroup({});
  loading: boolean = false;
  submitted = false;
  showAlert: boolean = false;
  alertMessage: string | undefined;
  forcaDaSenha: string | undefined;
  success: boolean | undefined;
  retorno!: Retorno<Cliente>;
  closeResult: string;
  endereco: ClienteEndereco;

  constructor(
    private cacheService: CacheService,
    private cookieService: CookieService,
    private profileService: ProfileService,
    private formBuilder: FormBuilder,
    private modalService: NgbModal,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loading = true;

    this.CreateForm();

    let user: Cliente = JSON.parse(this.cookieService.get('emb_user'));

    this.cacheService
      .GetFromCache<Cliente>(user.Id)
      .pipe(finalize(() => (this.loading = false)))
      .subscribe(
        (s) => {
          this.cliente = s;
          this.CreateForm();
        },
        (err) => {
          console.log(err)
        },
        () => {
          if (!this.cliente) {
            this.profileService.GetById(user.Id).subscribe(
              (s) => {
                this.cliente = s;
                this.CreateForm();
              },
              (err: HttpErrorResponse) => {
                console.log(err)
                if (err.status == 0) {
                } else {
                }
              }
            );
          }
        }
      );
  }

  get f() {
    return this.registerForm.controls;
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

  CreateForm() {
    this.registerForm = this.formBuilder.group({
      Nome: [
        this.cliente?.Nome,
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(100),
        ],
      ],
      Sobrenome: [
        this.cliente?.Sobrenome,
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(100),
        ],
      ],
      Cpf: [
        this.cliente?.Cpf,
        [
          Validators.required,
          Validators.minLength(11),
          Validators.maxLength(11),
        ],
      ],
      Celular: [
        this.cliente?.Celular,
        [Validators.minLength(11), Validators.maxLength(11)],
      ],
      DataNascimento: [
        moment(this.cliente?.DataNascimento).format('YYYY-MM-DD')?.toString(),
        Validators.required,
      ],
      Email: [this.cliente?.Email, [Validators.required, Validators.email]],
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

  Salvar() {
    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    }

    this.loading = true;

    this.cliente = Object.assign({}, this.cliente, this.registerForm.value);

    this.profileService.Update(this.cliente).subscribe(
      (success) => {
        this.retorno = success;
        this.alertMessage = this.retorno.Mensagem;
        this.showAlert = true;
        this.success = true;
        this.loading = false;
      },
      (err) => {
        this.retorno = err.error;
        this.alertMessage = this.retorno.Mensagem;
        this.showAlert = true;
        this.success = false;
        this.loading = false;
      }
    );
  }

  Open(content: any, endereco: ClienteEndereco) {
    this.endereco = endereco;
    this.modalService.open(content, { centered: true });
  }
}
