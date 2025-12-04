import * as grpc from "@grpc/grpc-js";
import { apostaProto, competicaoProto, integrantesProto } from "../grpc/Clients";
import { Request, Response } from "express";
import { promisify } from "util"
export class ApostasController {
    _apostaClient: grpc.GrpcObject
    _competicaoClient: grpc.GrpcObject
    _integrantesClient: grpc.GrpcObject
    constructor() {
        this._apostaClient = new apostaProto.Comunicacao.Grpc.ApostaService("localhost:5183", grpc.credentials.createInsecure())
        this._competicaoClient = new competicaoProto.Comunicacao.Grpc.CompeticaoService("localhost:5183", grpc.credentials.createInsecure())
        this._integrantesClient = new integrantesProto.Comunicacao.Grpc.IntegrantesService("localhost:5183", grpc.credentials.createInsecure())

    }

    async Listar(req: Request, res: Response) {
        try {// Buscar apostas
            const listarApostaAsync = promisify(this._apostaClient.Listar).bind(this._apostaClient);
            const listarCompeticaoAsync = promisify(this._competicaoClient.Listar).bind(this._competicaoClient)



            let apostaResult = await listarApostaAsync({}); // ApostaEmpty = objeto vazio
            let competicaoResult = await listarCompeticaoAsync({})


            console.log(apostaResult)
            console.log(competicaoResult)
            if (apostaResult && competicaoResult) {
                apostaResult = Object.values(apostaResult.apostas).flatMap(x => {
                    const competicao = Object.values(competicaoResult.competicoes).filter(y => y.id === x.competicaoId).map(y => y.nome)[0]
                    console.log(x)
                    return {
                        id: x.id,
                        competicaoId: competicao,
                        valor: x.valor,
                        multiplicador: x.multiplicador
                    }
                })
            }

            return res.status(200).json({ apostas: apostaResult, ...competicaoResult });
        } catch (err) {
            return res.status(500).json({ message: err })
        }
    }

    async Inserir(req: Request, res: Response) {
        try {
            if (!req.body.competicaoId)
                return res.status(400).json({ message: "Competição é obrigatória" })
            if (!req.body.valor) {
                return res.status(400).json({ message: "O valor é obrigatório" })
            }
            const request = {
                competicaoId: Number(req.body.competicaoId),
                valor: Number(req.body.valor)
            };

            const apostaResult = await new Promise((resolve, reject) => {
                this._apostaClient.Inserir(request, (err: any, response: any) => {
                    if (err) return reject(err);
                    resolve(response);
                });
            });

            res.status(200).json(apostaResult);
        } catch (err) {
            res.status(500).json({ error: err });
        }
    }
}