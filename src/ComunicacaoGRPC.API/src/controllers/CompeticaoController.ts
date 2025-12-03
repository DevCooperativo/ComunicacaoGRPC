import * as grpc from "@grpc/grpc-js";
import { competicaoProto, integrantesProto } from "../grpc/Clients";
import { Request, Response } from "express";
import { promisify } from "util"
import { Timestamp } from 'google-protobuf/google/protobuf/timestamp_pb';
import { timeStamp } from "console";

export class CompeticaoController {
    _competicaoClient: grpc.GrpcObject
    _integrantesClient: grpc.GrpcObject
    constructor() {
        this._competicaoClient = new competicaoProto.Comunicacao.Grpc.CompeticaoService("localhost:5183", grpc.credentials.createInsecure())
        this._integrantesClient = new integrantesProto.Comunicacao.Grpc.IntegrantesService("localhost:5183", grpc.credentials.createInsecure())

    }

    async Listar(req: Request, res: Response) {
        try {
            const listarCompeticaoAsync = promisify(this._competicaoClient.Listar).bind(this._competicaoClient)
            let competicaoResult = await listarCompeticaoAsync({})
            return res.status(200).json({ ...competicaoResult.competicoes });
        } catch (err) {
            return res.status(500).json({ message: err })
        }
    }


    async Inserir(req: Request, res: Response) {
        try {
            const { data, nome } = req.body;
            console.log("DATA RECEBIDA:", data);

            // "2053-06-12"
            const [year, month, day] = data.split("-").map(Number);

            // Criar data em UTC manualmente → 100% sem erro
            const jsDate = new Date(Date.UTC(year, month - 1, day));

            console.log("JS DATE:", jsDate, "TIME:", jsDate.getTime());

            const seconds = (Math.floor(jsDate.getTime() / 1000));
            const nanos = ((jsDate.getTime() % 1000) * 1e6);

            const request = {
                nome,
                data: {
                    seconds,
                    nanos
                }
            };

            const competicaoResult = await new Promise((resolve, reject) => {
                this._competicaoClient.Inserir(request, (err, response) => {
                    if (err) return reject(err);
                    resolve(response);
                });
            });

            return res.status(200).json(competicaoResult);

        } catch (err) {
            console.log(err);
            return res.status(500).json({ error: err.message ?? err });
        }
    }

}