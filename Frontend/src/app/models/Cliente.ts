export class Cliente {
    Id!: string
    Nome!: string
    Sobrenome!: string
    Cpf!: string
    Email!: string
    Senha!: string
    Celular: string | undefined    
    Ativo!: boolean
    DataNascimento!: Date
    DataCriacao!: Date    
}