import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ClienteEndereco } from '../models/ClienteEndereco';
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
        [Validators.minLength(100), Validators.maxLength(100)],
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
        (success) => {
          console.log(success);
        },
        (err) => {
          console.log(err)
        }
      );
  }

  Salvar() {
    this.submitted = true;

    this.addressService.UpdateAddress(this.endereco).subscribe(
      (success) => {},
      (err) => {
        console.log(err)        
      }
    );
  }
}
