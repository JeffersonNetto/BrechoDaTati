export class ClienteEndereco {
    Id!: number
    ClienteId!: string
    Logradouro!: string
    Numero: string | undefined
    Complemento: string | undefined
    Bairro: string | undefined
    Cidade!: string
    Uf!: string
    Cep: string | undefined
    Ativo!: boolean
    DataCriacao!: Date
}