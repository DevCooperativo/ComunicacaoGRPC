import * as grpc from "@grpc/grpc-js"
import * as protoLoader from "@grpc/proto-loader"
const integrantesPkgDef = protoLoader.loadSync(
    "./src/grpc/proto/integrantes.proto",
    {
        keepCase: true,
        longs: String,
        enums: String,
        defaults: true,
        oneofs: true,
    }
)
const integrantesProto = grpc.loadPackageDefinition(integrantesPkgDef) as any
const apostaPkgDef = protoLoader.loadSync(
    "./src/grpc/proto/aposta.proto",
    {
        keepCase: true,
        longs: String,
        enums: String,
        defaults: true,
        oneofs: true,
    }
)
const apostaProto = grpc.loadPackageDefinition(apostaPkgDef) as any
const competicaoPkgDef = protoLoader.loadSync(
    "./src/grpc/proto/competicao.proto",
    {
        keepCase: true,
        longs: String,
        enums: String,
        defaults: true,
        oneofs: true,
    }
)
const competicaoProto = grpc.loadPackageDefinition(competicaoPkgDef) as any

export { apostaProto, competicaoProto, integrantesProto }