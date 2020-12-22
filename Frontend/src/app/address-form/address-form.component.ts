import { HttpErrorResponse } from '@angular/common/http';
import { Component, ElementRef, Input, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { finalize } from 'rxjs/operators';
import { ClienteEndereco } from '../models/ClienteEndereco';
import { EnderecoViaCep } from '../models/EnderecoViaCep';
import { Retorno } from '../models/Retorno';
import { AddressService } from '../services/address.service';

@Component({
  selector: 'app-address-form',
  templateUrl: './address-form.component.html',
  styleUrls: ['./address-form.component.scss'],
})
export class AddressFormComponent implements OnInit {
  @Input() endereco: ClienteEndereco;  
  addressForm!: FormGroup;
  submitted: boolean = false;
  loading: boolean = false;
  showAlert: boolean = false;
  alertMessage: string | undefined;
  success: boolean | undefined;
  retorno!: Retorno<ClienteEndereco>;

  constructor(
    private addressService: AddressService,
    private formBuilder: FormBuilder
  ) {}

  ngOnInit(): void {
    this.addressForm = this.formBuilder.group({
      Logradouro: [
        this.endereco.Logradouro,
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(150),
        ],
      ],
      Numero: [
        this.endereco?.Numero,
        [Validators.minLength(1), Validators.maxLength(10)],
      ],
      Complemento: [
        this.endereco?.Complemento,
        [Validators.minLength(1), Validators.maxLength(100)],
      ],
      Bairro: [
        this.endereco?.Bairro,
        [Validators.minLength(2), Validators.maxLength(100)],
      ],
      Cidade: [
        this.endereco.Cidade,
        [
          Validators.required,
          Validators.minLength(2),
          Validators.maxLength(100),
        ],
      ],
      Uf: [
        this.endereco.Uf,
        [Validators.required, Validators.minLength(2), Validators.maxLength(2)],
      ],
      Cep: [
        this.endereco.Cep,
        [Validators.minLength(8), Validators.maxLength(8)],
      ],
    });
  }

  get f() {
    return this.addressForm.controls;
  }

  GetAddressViaCep() {
    this.addressService
      .GetAddressViaCep(this.addressForm.get('Cep').value)
      .subscribe(
        (success: EnderecoViaCep) => {
          this.addressForm.get('Logradouro').setValue(success.logradouro);
          this.addressForm.get('Numero').setValue(null);
          this.addressForm.get('Complemento').setValue(success.complemento);
          this.addressForm.get('Bairro').setValue(success.bairro);
          this.addressForm.get('Cidade').setValue(success.localidade);
          this.addressForm.get('Uf').setValue(success.uf);          
        },
        (err) => {
          console.log(err);
        }
      );
  }

  Salvar() {
    this.submitted = true;    

    if (this.addressForm.invalid) {
      return;
    }

    this.loading = true;

    Object.assign(this.endereco, this.addressForm.value);

    console.log(this.endereco);

    this.addressService
      .UpdateAddress(this.endereco)
      .pipe(
        finalize(() => {
          this.loading = false;
          this.showAlert = true;
        })
      )
      .subscribe(
        (success) => {
          this.retorno = success;
          this.alertMessage = this.retorno.Mensagem;          
          this.success = true;
        },
        (err: HttpErrorResponse) => {

          if (err.status == 0) {
            this.alertMessage =
              'Sistema temporariamente indisponível. Tente novamente mais tarde.';
          } else if (err.status > 0) {
            this.retorno = err.error;
            this.alertMessage = this.retorno.Mensagem;
          }
          
          this.success = false;          
        },  
      );
  }
}
