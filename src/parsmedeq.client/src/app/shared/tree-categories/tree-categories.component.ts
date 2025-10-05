import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Tree} from '../../../lib/models/MenusResponse';

@Component({
  selector: 'app-tree-categories',
  templateUrl: './tree-categories.component.html',
  styleUrls: ['./tree-categories.component.scss']
})
export class TreeCategoriesComponent implements OnInit {
  @Input() parent: Tree;
  @Input() isTopLevel: boolean;
  @Input() selectedId: string;
  isOpen: { [id: string]: boolean; } = {};
  @Output() onItemClicked = new EventEmitter<Tree>();

  constructor() {
  }

  ngOnInit(): void {
  }

  clickMenu(item: Tree) {
    if (item.children?.length > 0) {
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
