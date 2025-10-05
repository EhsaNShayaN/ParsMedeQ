import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Tree} from '../../core/models/MenusResponse';

@Component({
  selector: 'app-tree-categories',
  templateUrl: './tree-categories.component.html',
  styleUrls: ['./tree-categories.component.scss'],
  standalone: false,
})
export class TreeCategoriesComponent implements OnInit {
  @Input() parent!: Tree;
  @Input() isTopLevel: boolean = false;
  @Input() selectedId: number = 0;
  isOpen: { [id: string]: boolean; } = {};
  @Output() onItemClicked = new EventEmitter<Tree>();

  constructor() {
  }

  ngOnInit(): void {
  }

  clickMenu(item: Tree) {
    if ((item.children?.length ?? 0) > 0) {
      this.openSubMenu(item);
    } else {
      this.onItemClicked.emit(item);
    }
  }

  openSubMenu(item: Tree) {
    this.isOpen[item.id] = !this.isOpen[item.id];
  }

  clickItem(child: Tree) {
    this.onItemClicked.emit(child);
  }
}
