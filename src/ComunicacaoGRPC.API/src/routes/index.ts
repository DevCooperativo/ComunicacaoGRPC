import { Router } from "express";
import { ApostasController } from "../controllers/ApostasController";
import { CompeticaoController } from "../controllers/CompeticaoController";
import { IntegrantesController } from "../controllers/IntegrantesController";

const routes = Router()

const apostaController = new ApostasController()
const competicaoController = new CompeticaoController()
const integrantesController = new IntegrantesController()
routes.get("/apostas", (req, res) => apostaController.Listar(req, res))
routes.post("/apostas", (req, res) => apostaController.Inserir(req, res))
routes.get("/competicoes", (req, res) => competicaoController.Listar(req, res))
routes.post("/competicoes", (req, res) => competicaoController.Inserir(req, res))
routes.get("/integrantes", (req, res) => integrantesController.Listar(req, res))
export { routes }