import { ClienteEndereco } from "./ClienteEndereco"
import { Usuario } from "./Usuario"

export class Cliente extends Usuario {
    Id!: string
    Nome!: string
    Sobrenome!: string
    Cpf!: string    
    Celular: string | undefined    
    Ativo!: boolean
    DataNascimento!: Date
    DataCriacao!: Date    
    ClienteEndereco: ClienteEndereco[] | undefined
}