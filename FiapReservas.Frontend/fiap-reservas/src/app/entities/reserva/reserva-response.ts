import { RestauranteResponse } from "../restaurante/restaurante-response";

export class ReservaResponse {
    id: string = '';
    restaurante : RestauranteResponse = new RestauranteResponse();
    dataReserva : Date  = new Date();
    nome : string  = '';
    telefone : string  = '';
    email : string  = '';
    quantidadePessoas : Number  = 0;
}
