import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { first } from 'rxjs/operators';
import { Cliente } from '../models/Cliente';
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
  loading = false;
  submitted = false;
  cliente!: Cliente
  termos: boolean = false  

  constructor(
    private formBuilder: FormBuilder,
    private registerService: RegisterService
    ) {}

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      Nome: ['', Validators.required],
      Sobrenome: ['', Validators.required],
      Cpf: ['', Validators.required],
      DataNascimento: ['', Validators.required],
      Email: ['', Validators.required],
      Senha: ['', Validators.required],
    });
  }

  AceitarTermos(){
    this.termos = !this.termos;
  }

  Cadastrar() {

    this.submitted = true;

    // stop here if form is invalid
    if (this.registerForm.invalid) {
      return;
    } 

    this.loading = true;

    console.log(this.registerForm.value)

    this.cliente = Object.assign({}, this.cliente, this.registerForm.value);

    console.log(this.cliente)

    this.registerService
      .Register(this.cliente)
      .pipe(first())
      .subscribe(
        (data) => {
          console.log(data)
          
        },
        (err) => {
          console.warn(err)
          if (err && err.error && err.error.message) {
          } else {
          }

          this.loading = false;
        }
      );

  }
}
