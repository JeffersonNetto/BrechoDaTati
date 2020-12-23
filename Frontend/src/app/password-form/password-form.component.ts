import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MustMatch } from '../helpers/must-match.validator';
import { AccountService } from '../services/account.service';
import { Helpers } from '../helpers/helpers'

@Component({
  selector: 'app-password-form',
  templateUrl: './password-form.component.html',
  styleUrls: ['./password-form.component.scss'],
})
export class PasswordFormComponent implements OnInit {
  passwordForm: FormGroup;
  submitted: boolean = false;
  loading: boolean = false;
  showAlert: boolean = false;
  alertMessage: string | undefined;
  success: boolean | undefined;
  forcaDaSenha: string | undefined;

  constructor(
    private formBuilder: FormBuilder,
    private accountService: AccountService
  ) {}

  ngOnInit(): void {
    this.passwordForm = this.formBuilder.group({
      SenhaAtual: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(100),
        ],
      ],
      NovaSenha: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(100),
        ],
      ],
      Confirmacao: [
        '',
        [
          Validators.required,
          Validators.minLength(6),
          Validators.maxLength(100),          
        ],
      ],
    }, {
      validator: MustMatch('NovaSenha','Confirmacao')
    });
  }

  get f() {
    return this.passwordForm.controls;
  }

  Salvar() {

    this.submitted = true;

  }

  VerificarForcaDaSenha() {
    this.forcaDaSenha = Helpers.VerificarForcaDaSenha(this.f.NovaSenha.value)   
  }
}
