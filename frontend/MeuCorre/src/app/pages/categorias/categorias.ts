import { Component, inject, OnInit, signal, TemplateRef, WritableSignal } from '@angular/core';
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
export class Categorias implements OnInit {
 
  private modalService = inject(NgbModal);
  private CategoriaService = inject(CategoriaService);
  closeResult: WritableSignal<string> = signal('');

  nome = new FormControl('');
  descricao = new FormControl('');
  cor = new FormControl('');
  icone = new FormControl('');

  active = 1;
  editandoCategoria = false;
  idEditandoCategoria = '';


listaCategorias = signal<CategoriaModel[]>([]);







  get listaReceitas(): CategoriaModel[] {
    return this.listaCategorias().filter((categoria) => categoria.tipo === 'receita');
  }

  get listaDespesas(): CategoriaModel[] {
    return this.listaCategorias().filter((categoria) => categoria.tipo === 'despesa');
  }

 ngOnInit(): void {
    this.carregarTodasCategorias();
  }

  carregarTodasCategorias() {
    this.CategoriaService.obterTodasPorUsuario().subscribe({
      next: (dados) => {
        this.listaCategorias.set(dados);
      },
     
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
    } else {
      this.editandoCategoria = false;
      this.nome.reset();
      this.descricao.reset('');
      this.cor.reset('');
      this.icone.reset('');
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

  cadastrarCategoria() {
    if (this.active === 1) {
      this.listaCategorias().push({
        id: this.listaCategorias.length + 1 + '',
        nome: this.nome.value!,
        descricao: this.descricao.value!,
        cor: this.cor.value!,
        icone: this.icone.value!,
        ativo: true,
        tipo: 'despesa',
      });
    } else {
      this.listaCategorias().push({
        id: this.listaCategorias.length + 1 + '',
        nome: this.nome.value!,
        descricao: this.descricao.value!,
        cor: this.cor.value!,
        icone: this.icone.value!,
        ativo: true,
        tipo: 'receita',
      });
    }

    this.modalService.dismissAll();
  }

  excluirCategoria(id: string) {
    this.listaCategorias = this.listaCategorias().filter(
      (categoria) => categoria.id !== id.toString()
    );
  }

  editarCategoria() {
    const categoria = this.listaCategorias().find((c) => c.id === this.idEditandoCategoria);
    if (categoria) {
      categoria.nome = this.nome.value!;
      categoria.descricao = this.descricao.value!;
      categoria.cor = this.cor.value!;
      categoria.icone = this.icone.value!;
    }
    console.log(this.listaCategorias);
    this.modalService.dismissAll();
  }
}
