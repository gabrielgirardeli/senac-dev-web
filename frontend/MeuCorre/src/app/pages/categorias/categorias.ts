import { Component, computed, inject, OnInit, signal, TemplateRef, WritableSignal } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import {
  ModalDismissReasons,
  NgbModal,
  NgbNavModule,
  NgbTooltipModule,
} from '@ng-bootstrap/ng-bootstrap';
import { IconAvatar } from '../../shared/components/icon-avatar/icon-avatar';
import { StatusBadge } from '../../shared/components/status-badge/status-badge';
import { CategoriaModel } from './models/categoria.model';
import { CategoriaService } from './categoria.service';

@Component({
  selector: 'app-categorias',
  imports: [NgbNavModule, IconAvatar, StatusBadge, ReactiveFormsModule, NgbTooltipModule],
  templateUrl: './categorias.html',
  styleUrl: './categorias.css',
})
export class Categorias implements OnInit  {

  private modalService = inject(NgbModal);
  private categoriaService = inject(CategoriaService);

  closeResult: WritableSignal<string> = signal('');

  nome = new FormControl('');
  descricao = new FormControl('');
  cor = new FormControl('');
  icone = new FormControl('');
  tipo = new FormControl('despesa');

  active = 1;
  editandoCategoria = false;
  idEditandoCategoria = '';

  listaCategorias = signal<CategoriaModel[]>([]);

  // Listas derivadas (automáticas) para separar receitas e despesas
  listaReceitas = computed(() => this.listaCategorias().filter((c) => c.tipo === 'receita'));
  listaDespesas = computed(() => this.listaCategorias().filter((c) => c.tipo === 'despesa'));

  ngOnInit(): void {
    // Ao iniciar a tela, buscamos as categorias do usuário
    this.carregarCategorias();
  }


  // Busca as categorias no backend e garante que o campo "tipo" fique em string
  carregarCategorias(){
    this.categoriaService.obterTodasPorUsuario().subscribe({
      next:(dados) => {
        // Mapeia os dados para o modelo do frontend
        const categoriasMapeadas = dados.map((c) => {
          const item: CategoriaModel = {
            id: c.id,
            nome: c.nome,
            descricao: c.descricao,
            cor: c.cor,
            icone: c.icone,
            // Normaliza 'tipo' para string para o frontend
            tipo: c.tipo === '1' ? 'receita' : 'despesa',
            ativo: c.ativo,
          };
        return item;
        });
        this.listaCategorias.set(categoriasMapeadas);
      },
      error: (err) => {
        console.error('Erro ao carregar categorias:', err);
      }
    })
  }

  open(content: TemplateRef<any>, categoria?: CategoriaModel) {
    if (categoria) {
      this.idEditandoCategoria = categoria.id;
      this.editandoCategoria = true;
      this.nome.setValue(categoria.nome);
      this.descricao.setValue(categoria.descricao);
      this.cor.setValue(categoria.cor);
      this.icone.setValue(categoria.icone);
      this.tipo.setValue(categoria.tipo);
    } else {
      this.editandoCategoria = false;
      this.nome.reset();
      this.descricao.reset('');
      this.cor.reset('');
      this.icone.reset('');
      this.tipo.setValue('despesa');
    }
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then(
      (result) => {},
      (reason) => {
        this.closeResult.set(`Dismissed ${this.getDismissReason(reason)}`);
      }
    );
  }

  private getDismissReason(reason: any): string {
    switch (reason) {
      case ModalDismissReasons.ESC:
        return 'by pressing ESC';
      case ModalDismissReasons.BACKDROP_CLICK:
        return 'by clicking on a backdrop';
      default:
        return `with: ${reason}`;
    }
  }

  // Cadastra uma nova categoria (apenas no frontend por enquanto)
  cadastrarCategoria() {
    const novaCategoria: CategoriaModel = {
      id: (this.listaCategorias().length + 1).toString(),
      nome: this.nome.value ?? '',
      descricao: this.descricao.value ?? '',
      cor: this.cor.value ?? '#000000',
      icone: this.icone.value ?? 'ri-question-line',
      // Mantém 'tipo' como string no frontend para filtragem
      tipo: this.tipo.value ?? 'despesa',
      ativo: true,
    };
    // TODO: Chamar o serviço para cadastrar no backend e depois atualizar a lista
    this.listaCategorias.update(categorias => [...categorias, novaCategoria]);
    this.modalService.dismissAll();
  }

  // Remove uma categoria da lista pelo id (frontend)
  excluirCategoria(id: string) {
    this.listaCategorias.update(categorias => categorias.filter(c => c.id !== id));
  }

  // Edita a categoria selecionada usando os valores do formulário (frontend)
  editarCategoria() {
    this.listaCategorias.update(categorias =>
      categorias.map(c => {
        if (c.id !== this.idEditandoCategoria) {
          return c;
        }
        const atualizado: CategoriaModel = {
          id: c.id,
          nome: this.nome.value ?? c.nome,
          descricao: this.descricao.value ?? c.descricao,
          cor: this.cor.value ?? c.cor,
          icone: this.icone.value ?? c.icone,
          // Mantém 'tipo' como string no frontend para filtragem
          tipo: this.tipo.value ?? c.tipo,
          ativo: c.ativo,
        };
        return atualizado;
      })
    );
    this.modalService.dismissAll();
  }
}